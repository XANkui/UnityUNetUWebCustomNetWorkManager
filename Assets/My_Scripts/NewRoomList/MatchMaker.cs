using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class MatchMaker : MonoBehaviour
{

    NetworkManager manager;

    public string roomName;

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    GameObject btn;
    [SerializeField]
    Transform parent;

    private void Start()
    {
        //manager = NetworkManager.singleton;
        manager = PlayerSpawn.singleton;
        if (manager.matchMaker == null)
            manager.StartMatchMaker();

        InvokeRepeating("OnRefreshBtn", 1, 1);
    }

    
    public void SetRoomName(string name)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(name)) {
            name = "Room " + Random.Range(0.0f,1000.0f);
        }

        roomName = name;
    }

    public void OnCreateRoomBtn()
    {
        manager.matchMaker.CreateMatch(roomName, 2, true, "", "", "", 0, 0, manager.OnMatchCreate);
    }

    public void OnRefreshBtn()
    {
        manager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
    }


    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if (!success)
        {
            Debug.Log("error");
            return;
        }
        ClearList();
        foreach (var match in matches)
        {
            GameObject go = Instantiate(btn, parent);
            roomList.Add(go);
            go.GetComponent<JoinButton>().SetUp(match);
        }

    }

    void ClearList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }
        roomList.Clear();
    }
}