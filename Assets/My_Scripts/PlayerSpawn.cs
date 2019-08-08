using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawn : NetworkManager
{



    int spawnCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //原来的OnServerAddPlayer函数
    //public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    //{
    //    var player = (GameObject)GameObject.Instantiate(playerPrefab, playerSpawnPos, Quaternion.identity);
    //    NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    //}

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Transform tmpTr = GetPlayerPosition();
        GameObject player = GameObject.Instantiate(playerPrefab, tmpTr.position, Quaternion.identity, tmpTr) as GameObject;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);

    }

    public Transform GetPlayerPosition()
    {

        Debug.Log("PlayerSpawn / GetPlayerPosition");
        spawnCount = startPositions.Count;
        for (int i =0; i < spawnCount; i++)
        {
            if (startPositions[i].childCount == 0) {

                Debug.Log("PlayerSpawn / GetPlayerPosition / spawnCount: " + spawnCount +" i = "+i);

                return startPositions[i];

                
            }
        }

        return null;
     
    }

}
