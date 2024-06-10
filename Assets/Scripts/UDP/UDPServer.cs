using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class UDPServer : MonoBehaviour
{
    private UdpClient udpClient;

    private void Start()
    {
        InitUDPServer();
    }

    [Button]
    public void InitUDPServer()
    {
        Debug.Log($"UDP Server Online From Port : {CommunicationManager.Instance.udpServerPort}");
        udpClient = new UdpClient(CommunicationManager.Instance.udpServerPort);
        udpClient.EnableBroadcast = true;
        ListenAsync();
    }

    private async void ListenAsync()
    {
        try
        {
            while (true)
            {
                Debug.Log("UDP Server on Duty");
                UdpReceiveResult result = await udpClient.ReceiveAsync();
                ProcessReceivedPacket(result);
            }
        }
        catch (ObjectDisposedException)
        {
            // Socket has been closed
            Debug.Log("Socket closed.");
        }

        Debug.Log("UDP Server Offline");
    }

    private void ProcessReceivedPacket(UdpReceiveResult result)
    {
        string message = Encoding.UTF8.GetString(result.Buffer);
        Debug.Log("Received: [" + message + "] from " + result.RemoteEndPoint.Address.ToString());


        if (message == "I'm Here!" && CommunicationManager.Instance.isFindingServer)
            CommunicationManager.Instance.AddNewServer(result.RemoteEndPoint.Address.ToString(), result.RemoteEndPoint.Address.ToString());

        // Sending a unicast response
        if (message != "I'm Here!" && CommunicationManager.Instance.isServerHost)
            CommunicationManager.Instance.udpClient.SendUnicast("I'm Here!", result.RemoteEndPoint);
    }

    void OnApplicationQuit()
    {
        if (udpClient != null)
            udpClient.Close();
    }
}