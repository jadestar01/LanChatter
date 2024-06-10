using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Sirenix.OdinInspector;
using JetBrains.Annotations;
using Unity.VisualScripting;

/// <summary>
/// Create Server
/// </summary>
public class Server : MonoBehaviour
{
    /*
    private TcpListener tcpListener;
    private Thread tcpListenerThread;
    private TcpClient connectedTcpClient;

    [Button]
    private void InitServer()
    {
        Debug.Log("Start Server");

        //Start Server on new Thread
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequest));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    //Backgorund run server
    private void ListenForIncommingRequest()
    {
        try
        {
            //Set new server to (GetLocalIP, GetFreePort)
            tcpListener = new TcpListener(CommunicationManager.Instance.GetLocalIP(),
                                          CommunicationManager.Instance.tcpPort);
            tcpListener.Start();
            Debug.Log("Server is listening");

            while (true)
            {
                using (connectedTcpClient = tcpListener.AcceptTcpClient())
                {
                    //Get a stream object for reading
                    using (NetworkStream stream = connectedTcpClient.GetStream())
                    {
                        //Read incomming stream into byte array.
                        do
                        {
                            //TODO
                        } while (true);
                    }
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
    }
    */
}