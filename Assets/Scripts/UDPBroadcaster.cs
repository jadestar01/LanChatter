using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Threading.Tasks;
using System.Threading;

public class UDPBroadcaster : MonoBehaviour
{
    /*
    private Thread listenThread;
    private bool isRunning = true;

    public List<string> ipAddresses = new List<string>();
    

    [Button]
    public void Broadcast()
    {
        UdpClient client = new UdpClient();
        IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, CommunicationManager.Instance.udpPort);

        try
        {
            client.EnableBroadcast = true;
            byte[] bytes = Encoding.ASCII.GetBytes($"Hello World");
            client.Send(bytes, bytes.Length, ip);
            Debug.Log("UDP BroadCast");
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to send broadcast: {e.Message}");
        }
        finally
        {
            client.Close();
        }
    }

    [Button]
    public void InitUDPServer()
    {
        listenThread = new Thread(new ThreadStart(ListenForMessages));
        Debug.Log("UDP online");
        listenThread.IsBackground = true;  // Ensure the thread does not prevent the application from exiting
        listenThread.Start();
    }

    private void ListenForMessages()
    {
        UdpClient listener = new UdpClient(CommunicationManager.Instance.udpPort);
        IPEndPoint groupEP = new IPEndPoint(IPAddress.Broadcast, CommunicationManager.Instance.udpPort);
        listener.EnableBroadcast = true;

        try
        {
            while (isRunning)
            {
                // && CommunicationManager.Instance.isServerHost
                Debug.Log("whiling");
                if (listener.Available > 0)
                {
                    Debug.Log("get data");
                    //UDP 받은 경우
                    byte[] bytes = listener.Receive(ref groupEP);
                    string receivedMessage = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    Debug.Log($"[수신 from {groupEP.Address}] {receivedMessage}");

                    // Send back our IP address
                    UdpClient responder = new UdpClient();
                    string response = $"My IP address is: {groupEP.Address.ToString()}";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    responder.Send(responseBytes, responseBytes.Length, groupEP);
                    Debug.Log($"[송신 to : {groupEP.Address}]: {response}");

                    string sourceIP = groupEP.Address.ToString();
                    //내 IP가 아니라면,
                    if (!ipAddresses.Contains(sourceIP))
                    {
                        if(!CommunicationManager.Instance.localAddressed.Contains(sourceIP))
                        {
                            ipAddresses.Add(sourceIP);  // 새로운 IP 주소 추가
                            CommunicationManager.Instance.openServers = ipAddresses;
                        }
                    }
                    

                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Exception caught: {e}");
        }
        finally
        {
            Debug.Log("UDP offline");
            listener.Close();
        }
    }

    private void OnApplicationQuit()
    {
        isRunning = false;
                if (listenThread != null)
        {
            listenThread.Join();  // Wait for the thread to finish   
        }
    }
    */
}