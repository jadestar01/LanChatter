using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Windows;

public class TCPServer : MonoBehaviour
{
    private TcpListener tcpListener;
    private List<TcpClient> connectedClients = new List<TcpClient>();

    // 서버 시작
    [Button]
    public void InitServer()
    {
        CommunicationManager.Instance.isServerHost = true;

        tcpListener = new TcpListener(IPAddress.Any, CommunicationManager.Instance.tcpServerPort);
        tcpListener.Start();
        Debug.Log($"TCP Server Online From Port : {CommunicationManager.Instance.tcpServerPort}");

        Thread acceptThread = new Thread(AcceptClients);
        acceptThread.Start();
    }

    [Button]
    // 서버 종료
    public void StopServer()
    {
        CommunicationManager.Instance.isServerHost = false;
        if(tcpListener != null)
            tcpListener.Stop();

        BroadcastMessageToClients(CommunicationManager.Instance.endMessage);

        // 모든 클라이언트 연결 종료
        foreach (TcpClient client in connectedClients)
        {
            client.Close();
        }
        connectedClients.Clear();
        Debug.Log("Server stopped.");
    }


    // 클라이언트 수락
    private void AcceptClients()
    {
        while (CommunicationManager.Instance.isServerHost)
        {
            try
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                if (CommunicationManager.Instance.isServerHost)  // Check again in case server is stopping
                {
                    connectedClients.Add(client);
                    Debug.Log("Client connected: " + client.Client.RemoteEndPoint);

                    Thread clientThread = new Thread(() => HandleClient(client));
                    clientThread.Start();
                }
                else
                {
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Debug.Log("Listener socket closed with exception: " + e.Message);
            }
        }
    }

    // 클라이언트 처리
    private void HandleClient(TcpClient client)
    {
        string nickName = "";
        try
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byteCount;

            while ((byteCount = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, byteCount);
                BroadcastMessageToClients(message);
                Debug.Log("Received: " + message);
                if(Equals(nickName, ""))
                    nickName = GetNickName(message);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error in client handling: " + e.Message);
        }
        finally
        {        
            client.Close();
            connectedClients.Remove(client);
            Debug.Log("Client disconnected.");
            if(CommunicationManager.Instance.isServerHost)
                BroadcastMessageToClients(nickName + CommunicationManager.Instance.mask + "-----");
        }
    }

    string GetNickName(string input)
    {
        string[] parts = input.Split(new string[] { CommunicationManager.Instance.mask }, StringSplitOptions.None);

        if (parts.Length == 2)
        {
            string firstPart = parts[0];
            string secondPart = parts[1];

            return firstPart;
        }

        return "";
    }

    // 메시지 방송
    public void BroadcastMessageToClients(string message)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(message);
        foreach (TcpClient client in connectedClients)
        {
            if (client.Connected)
            {
                NetworkStream stream = client.GetStream();
                stream.Write(buffer, 0, buffer.Length);
            }
        }

        UnityMainThreadDispatcher.Instance.Enqueue(() => CommunicationManager.Instance.chatGenerator.GenerateChat(message));
    }

    private void OnApplicationQuit()
    {
        StopServer();
    }
}