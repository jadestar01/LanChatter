using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGenerator : MonoBehaviour
{
    public Transform chatPlate;
    public GameObject chat;

    [Button]
    public void ClearChat()
    {
        for (int i = 0; i < chatPlate.transform.childCount; i++)
            Destroy(chatPlate.GetChild(chatPlate.transform.childCount - 1 - i).gameObject);
    }

    public void GenerateChat(string input)
    {
        string[] parts = input.Split(new string[] { CommunicationManager.Instance.mask }, StringSplitOptions.None);

        if (parts.Length == 2)
        {
            string firstPart = parts[0];
            string secondPart = parts[1];

            Debug.Log("FirstPart = " + firstPart);
            Debug.Log("SecondPart = " + secondPart);

            //���� �޽���
            if (Equals(secondPart, "+++++"))
            {
                secondPart = firstPart + "���� �����ϼ̽��ϴ�.";
                firstPart = "������";
            }
            //���� �޽���
            else if (Equals(secondPart, "-----"))
            {
                secondPart = firstPart + "���� �����ϼ̽��ϴ�.";
                firstPart = "������";
            }

            GenChat(firstPart, secondPart);
        }
    }

    private void GenChat(string nickName, string str)
    {
        GameObject ch = Instantiate(chat);
        ch.transform.SetParent(chatPlate);
        ch.GetComponent<Text>().Init(nickName, str);
    }
}
