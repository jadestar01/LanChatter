using Sirenix.OdinInspector;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TCPCommunicator : MonoBehaviour
{
    /*
    private TcpListener tcpListener;
    private Thread tcpListenerThread;

    [Button]
    public void InitServer()
    {
        CommunicationManager.Instance.isServerHost = true;
        tcpListenerThread = new Thread(new ThreadStart(ListenForIncomingRequests));
        tcpListenerThread.IsBackground = true;
        tcpListenerThread.Start();
    }

    private void ListenForIncomingRequests()
    {
        try
        {
            tcpListener = new TcpListener(IPAddress.Any, CommunicationManager.Instance.tcpPort);
            tcpListener.Start();
            Debug.Log("TCP online" + IPAddress.Any);

            TcpClient connectedTcpClient = tcpListener.AcceptTcpClient();

            while (true)
            {
                NetworkStream stream = connectedTcpClient.GetStream();
                if (stream.CanRead)
                {
                    byte[] myReadBuffer = new byte[1024];
                    StringBuilder completeMessage = new StringBuilder();
                    int numberOfBytesRead = 0;

                    // Read the incoming stream.
                    do
                    {
                        numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
                        completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                    }
                    while (stream.DataAvailable);

                    Debug.Log("Received : " + completeMessage);

                    // Send a response.
                    byte[] response = Encoding.ASCII.GetBytes("Hello from the server");
                    stream.Write(response, 0, response.Length);
                }
            }
        }
        catch (SocketException socketException)
        {
            Debug.Log("SocketException " + socketException.ToString());
        }
        finally
        {
            CommunicationManager.Instance.isServerHost = false;
        }
    }

    private void OnApplicationQuit()
    {
        tcpListener.Stop();
        tcpListenerThread.Abort();
    }

    [Button]
    private void ConnectToServer(string host)
    {
        try
        {
            TcpClient client = new TcpClient(host, CommunicationManager.Instance.tcpPort);

            Byte[] data = Encoding.ASCII.GetBytes("Hello from the client");

            NetworkStream stream = client.GetStream();

            // Send the message to the connected TcpServer.
            stream.Write(data, 0, data.Length);

            Debug.Log("Sent: Hello from the client");

            // Bytes Array to receive Server Response.
            data = new Byte[256];
            String responseData = String.Empty;

            // Read the first batch of the TcpServer response bytes.
            Int32 bytes = stream.Read(data, 0, data.Length);
            responseData = Encoding.ASCII.GetString(data, 0, bytes);
            Debug.Log("Received: " + responseData);

            stream.Close();
            client.Close();
        }
        catch (Exception e)
        {
            Debug.Log("Exception: " + e.Message);
        }
    }
    */
}