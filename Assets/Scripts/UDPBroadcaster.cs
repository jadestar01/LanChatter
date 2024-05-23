using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;

public class UDPBroadcaster
{
    private UdpClient udpClient;

    public void BroadcastMessage(int port)
    {
        udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Broadcast, port);

        byte[] bytes = Encoding.ASCII.GetBytes("Any servers out there?");
        udpClient.Send(bytes, bytes.Length, groupEP);
        Debug.Log("Broadcast message sent.");

        udpClient.Close();
    }
}