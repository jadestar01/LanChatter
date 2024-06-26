using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerUnit : MonoBehaviour
{
    public int index;
    public string serverIp;
    public TextMeshProUGUI serverName;
    public Image contour;

    //ServerUnit Prefab ����
    public void InitServerUnit(int index, string serverName, string serverIp)
    {
        this.serverName.text = serverName;
        this.serverIp = serverIp;
    }

    public void Select()
    {
        ServerList.Instance.DeselctAll();
        contour.gameObject.SetActive(true);
        ServerList.Instance.curServer = this;
        ServerList.Instance.selectedServer = index;
    }

    public void Deselect()
    {
        contour.gameObject.SetActive(false);
    }
}
