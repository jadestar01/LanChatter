using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Sirenix.OdinInspector;

public class UDPClient: MonoBehaviour
{
    private UdpClient udpClient;

    private void Start()
    {
        InitUDPClient();
    }

    [Button]
    public void InitUDPClient()
    {
        Debug.Log($"UDP Client Online From Port : {CommunicationManager.Instance.udpClientPort}");
        udpClient = new UdpClient(CommunicationManager.Instance.udpClientPort);
        udpClient.EnableBroadcast = true;
    }

    [Button]
    public void FindServer()
    {
        Debug.Log($"UDP Client Broadcast To Port : {CommunicationManager.Instance.udpServerPort}");
        SendBroadcast("Hello from client");
    }

    private async void SendBroadcast(string message)
    {
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Broadcast, CommunicationManager.Instance.udpServerPort);
        byte[] bytesToSend = Encoding.UTF8.GetBytes(message);
        await udpClient.SendAsync(bytesToSend, bytesToSend.Length, remoteEP);
    }

    public async void SendUnicast(string message, IPEndPoint ep)
    {
        IPEndPoint remoteEP = new IPEndPoint(ep.Address, CommunicationManager.Instance.udpServerPort);
        byte[] bytesToSend = Encoding.UTF8.GetBytes(message);
        await udpClient.SendAsync(bytesToSend, bytesToSend.Length, remoteEP);
        Debug.Log($"Sent unicast message to {ep.Address.ToString()}:{CommunicationManager.Instance.udpServerPort}");
    }

    void OnApplicationQuit()
    {
        if (udpClient != null)
            udpClient.Close();
    }
}