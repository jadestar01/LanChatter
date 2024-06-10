using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// User의 상태 분석
/// </summary>
public class Manager : MonoBehaviour
{
    public static Manager Instance { get; private set; }

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

    public GameObject mainMenu;
    public GameObject inServer;

    public State cur;           //정적    //현재 고정상태
    public State last;          //동적    //동적 변동상태

    private void Update()
    {
        if (cur != last)
        {
            cur = last;
            OnStateChanged(cur);
        }
    }

    private void OnStateChanged(State curState)
    {
        switch (curState)
        {
            case State.None:
                mainMenu.SetActive(true);
                inServer.SetActive(false);
                break;
            case State.Host:
            case State.Client:
                mainMenu.SetActive(false);
                inServer.SetActive(true);
                break;
            default: break;
        }
    }
}

public enum State
{
    None,       //Main Menu
    Host,       //Server Host
    Client      //Server Client
}