using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private Thread receiveThread;
    private volatile bool isConnected = false;

    [Button]
    public void ConnectToServer()
    {
        string ipAddress;
        Debug.Log("Trying Connect");
        if (!CommunicationManager.Instance.serverList.HasSelectedServer())
            return;

        ipAddress = CommunicationManager.Instance.serverList.GetServerIP();

        client = new TcpClient();
        client.Connect(ipAddress, CommunicationManager.Instance.tcpServerPort);
        stream = client.GetStream();
        isConnected = true;
        Debug.Log($"TCP Client Online From IP/Port : {ipAddress}/{CommunicationManager.Instance.tcpServerPort}");

        receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();
        CommunicationManager.Instance.isJoinServer = true;
    }

    private void ReceiveMessages()
    {
        byte[] buffer = new byte[1024];
        int bytesRead;

        SendMessageToServer(CommunicationManager.Instance.nickName + CommunicationManager.Instance.mask + "+++++");

        //Server Started 
        UnityMainThreadDispatcher.Instance.Enqueue(() => CommunicationManager.Instance.OnEnter());
        try
        {
            while (isConnected)
            {
                if (stream.DataAvailable)
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        UnityMainThreadDispatcher.Instance.Enqueue(() => Disconnect());
                        break;
                    }
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Debug.Log("Received: " + receivedData);

                    if (String.Equals(CommunicationManager.Instance.endMessage, receivedData))
                    {
                        UnityMainThreadDispatcher.Instance.Enqueue(() => Disconnect());
                        break;
                    }    

                    // 메인 스레드로 작업을 전환
                    UnityMainThreadDispatcher.Instance.Enqueue(() =>
                    {
                        CommunicationManager.Instance.chatGenerator.GenerateChat(receivedData);
                    });
                }
                else
                {
                    Thread.Sleep(100);  // 데이터가 없을 때 CPU 사용을 줄임
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Receive error: " + e.Message);
            isConnected = false; // 연결 상태 업데이트
        }
        finally
        {
            if (client != null)
                client.Close();
            if (stream != null)
                stream.Close();
        }
    }

    public void SendMessageToServer(string message)
    {
        if (isConnected && stream.CanWrite)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            stream.Write(buffer, 0, buffer.Length);
        }
    }

    public void Disconnect()
    {
        isConnected = false;
        CommunicationManager.Instance.isJoinServer = false;
        CommunicationManager.Instance.OnExit();

        if (stream != null)
        {
            stream.Close();
        }
        if (client != null)
        {
            client.Close();
        }

        StartCoroutine(WaitForThreadToFinish());
    }

    private IEnumerator WaitForThreadToFinish()
    {
        while (receiveThread != null && receiveThread.IsAlive)
        {
            yield return null; // Wait for one frame
        }
        CommunicationManager.Instance.OnExit();
        Debug.Log("End4");
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }
}