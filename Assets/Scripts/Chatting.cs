using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Sirenix.OdinInspector;

public class Chatting : MonoBehaviour
{
    public TMP_InputField inputField;

    public void Chat()
    {
        string str = CommunicationManager.Instance.nickName + "|+|" + inputField.text;
        inputField.text = "";

        Debug.Log(str);
        if (CommunicationManager.Instance.isServerHost)
            CommunicationManager.Instance.tcpServer.BroadcastMessageToClients(str);
        else
            CommunicationManager.Instance.tcpClient.SendMessageToServer(str);
    }
}
