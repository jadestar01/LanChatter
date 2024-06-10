using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using System.Net.NetworkInformation;
using JetBrains.Annotations;

public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager Instance { get; private set; }

    [Title("Connect Instances")]
    public ServerList serverList;
    public UDPServer udpServer;
    public UDPClient udpClient;

    [Title("Current Statment")]
    public bool isServerHost = false;
    public bool isFindingServer = false;

    [Title("Port Num")]
    public int udpClientPort = 7777;
    public int udpServerPort = 7778;
    public int tcpClientPort = 7779;
    public int tcpServerPort = 7780;

    [Title("Datas")]
    public List<string> localAddressed = new List<string>();
    public List<string> openServerNames = new List<string>();
    public List<string> openServers = new List<string>();

    private float loadingTime = 1f;
    private Coroutine cor;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Button]
    public void StartFindingServer()
    {
        serverList.ResetServerList();
        GetWifiIPAddress();
        isFindingServer = true;
        openServers.Clear();

        if (cor != null)
            StopCoroutine(cor);
        cor = StartCoroutine(FindServerCooltime());

        udpClient.FindServer();

        Debug.Log("Start Server Finding");
    }

    public void EndFindingServer()
    {
        Debug.Log("End Server Finding");

        serverList.SetServerList(openServerNames, openServers);
        isFindingServer = false;
    }

    [Button]
    public void AddNewServer(string serverName, string serverIp)
    {
        if (!openServers.Contains(serverIp) &&
           !localAddressed.Contains(serverIp))
        {
            openServerNames.Add(serverName);
            openServers.Add(serverIp);

            if (cor != null)
                StopCoroutine(cor);
            cor = StartCoroutine(FindServerCooltime());

            Debug.Log("Find new Server IP");
        }
    }

    IEnumerator FindServerCooltime()
    {
        yield return new WaitForSeconds(loadingTime);
        EndFindingServer();
    }

    [Button]
    public IPAddress GetLocalIP()
    {
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Debug.Log("local Ip = " + ip);
                return ip;
            }
        }

        return null;
    }

    [Button]
    public void GetWifiIPAddress()
    {
        localAddressed = new List<string>();
        Debug.Log("Searching for Local Wifi Address");
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            // 무선 인터페이스만 확인
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        localAddressed.Add(ip.Address.ToString());
    }
}