using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Find Lan Server
/// Use UDP BroadCast
/// </summary>

public class ServerList : MonoBehaviour
{
    public static ServerList Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public Transform serverUnitParent;
    public GameObject serverUnit;
    public List<ServerUnit> serverUnits = new List<ServerUnit>();
    public int selectedServer = 0;

    public ServerUnit curServer = null;

    [Button]
    public void SetServerList(List<string> servers, List<string> serverIps)
    {
        serverUnits.Clear();
        for (int i = 0; i < servers.Count; i++)
        {
            GameObject unit = Instantiate(serverUnit);
            unit.transform.SetParent(serverUnitParent);
            ServerUnit su =  unit.GetComponent<ServerUnit>();
            su.InitServerUnit(i, servers[i], serverIps[i]);
            serverUnits.Add(su);
        }
    }

    [Button]
    public void ResetServerList()
    {
        selectedServer = 0;
        serverUnits.Clear();
        for (int i = 0; i < serverUnitParent.childCount; i++)
            Destroy(serverUnitParent.GetChild(serverUnitParent.childCount - 1 - i).gameObject);
    }

    public void DeselctAll()
    {
        for (int i = 0; i < serverUnits.Count; i++)
            serverUnits[i].Deselect();
    }
}
