using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using TMPro;

public class CommunicationManager : MonoBehaviour
{
    public static CommunicationManager Instance { get; private set; }

    [Title("Connect Instances")]
    public ServerList serverList;
    public UDPServer udpServer;
    public UDPClient udpClient;
    public TCPServer tcpServer;
    public TCPClient tcpClient;
    public TMP_InputField serverNameField;
    public TMP_InputField nickNameField;
    public List<ContentSizeFitter> csfs;
    public ChatGenerator chatGenerator;
    public TextMeshProUGUI serverTitle;
    public Image block;

    [Title("Buttons")]
    public Button createServer;
    public Button exitServer;
    public Button findServer;
    public Button connectServer;

    [Title("Current Statment")]
    public bool isServerHost = false;
    public bool isFindingServer = false;
    public bool isJoinServer = false;

    [Title("Port Num")]
    public int udpClientPort = 7777;
    public int udpServerPort = 7778;
    public int tcpServerPort = 7779;

    [Title("Datas")]
    public List<string> localAddressed = new List<string>();
    public List<string> openServerNames = new List<string>();
    public List<string> openServers = new List<string>();

    [Title("Masking Word")]
    public string mask = "|+|";
    public string endMessage = "((!@)#!()@#(!)";

    private float loadingTime = 1f;
    private Coroutine cor;

    public string serverName;
    public string nickName;

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


    //클라이언트 입장 시
    public void OnEnter()
    {
        block.gameObject.SetActive(true);

        serverTitle.text = serverList.curServer.serverName.text;

        serverNameField.interactable = false;
        nickNameField.interactable = false;

        createServer.interactable = false;
        exitServer.interactable = true;
        findServer.interactable = false;
        connectServer.interactable = false;
    }

    //클라이언트 나갈 시
    public void OnExit()
    {
        Debug.Log("Called On Exit");

        block.gameObject.SetActive(false);

        serverTitle.text = "";
        chatGenerator.ClearChat();

        serverNameField.interactable = true;
        nickNameField.interactable = true;

        createServer.interactable = true;
        exitServer.interactable = false;
        findServer.interactable = true;
        connectServer.interactable = true;
    }

    public void AmIHost(bool host)
    {
        //서버 열 때,
        if (host)
        {
            block.gameObject.SetActive(true);

            serverTitle.text = serverName;
            chatGenerator.ClearChat();

            serverNameField.interactable = false;
            nickNameField.interactable = false;

            createServer.interactable = true;
            exitServer.interactable = false;
            findServer.interactable = false;
            connectServer.interactable = false;
        }
        //서버 닫을 때,
        else
        {
            block.gameObject.SetActive(false);

            serverTitle.text = "";
            chatGenerator.ClearChat();

            serverNameField.interactable = true;
            nickNameField.interactable = true;

            createServer.interactable = true;
            exitServer.interactable = false;
            findServer.interactable = true;
            connectServer.interactable = true;
        }
    }


    private void Start()
    {
        ResetCSF();
    }

    public void ResetCSF()
    {
        for(int i = 0; i < csfs.Count; i++)
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csfs[i].transform);
    }

    public void SetNickname() { if (!isJoinServer) nickName = nickNameField.text; }

    public void SetServername() { if (!isJoinServer) serverName = serverNameField.text; }

    [Button]
    public void StartFindingServer()
    {
        serverList.ResetServerList();
        GetWifiIPAddress();
        isFindingServer = true;
        openServers.Clear();
        openServerNames.Clear();

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