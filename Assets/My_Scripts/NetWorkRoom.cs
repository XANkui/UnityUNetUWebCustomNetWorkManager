using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetWorkRoom : MonoBehaviour {

    public void StartHost()
    {
        NetworkManager.singleton.StartHost();
    }
    public void StartClient()
    {
        NetworkManager.singleton.networkAddress = GameObject.Find("IPInputField").GetComponent<InputField>().text;
        NetworkManager.singleton.StartClient();
    }
    public void StopHost()
    {
        NetworkManager.singleton.StopHost();
    }


    public void OfflineSet()
    {
        GameObject.Find("CreateButton").GetComponent<Button>().onClick.AddListener(StartHost);
        GameObject.Find("JoinButton").GetComponent<Button>().onClick.AddListener(StartClient);
    }

    public void OnlineSet()
    {
        GameObject.Find("BackButton").GetComponent<Button>().onClick.AddListener(StopHost);

    }

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        switch (scene.buildIndex)
        {
            case 0:
                OfflineSet();
                break;
            case 1:
                OnlineSet();
                break;
            default:
                break;
        }
    }


    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
