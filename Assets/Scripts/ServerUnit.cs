using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ServerUnit : MonoBehaviour
{
    public int index;
    public TextMeshProUGUI serverName;
    public Image contour;

    //ServerUnit Prefab ¼³Á¤
    public void InitServerUnit(int index, string serverName)
    {
        this.serverName.text = serverName;
    }

    public void Select()
    {
        ServerList.Instance.DeselctAll();
        contour.gameObject.SetActive(true);
        ServerList.Instance.curServer = this;
    }

    public void Deselect()
    {
        contour.gameObject.SetActive(false);
    }
}
