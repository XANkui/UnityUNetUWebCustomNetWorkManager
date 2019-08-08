using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class QuitRoom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StopHost()
    {
        //NetworkManager.singleton.StopHost();
        PlayerSpawn.singleton.StopHost();
    }

    void OnApplicationQuit() {
        StopHost();
    }

    void OnDestroy() {
        StopHost();
    }
    void OnDisable() {
        StopHost();
    }
}
