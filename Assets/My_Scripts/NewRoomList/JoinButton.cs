using UnityEngine;

using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class JoinButton : MonoBehaviour
{

    NetworkManager manager;
    public Text nameTxt;
    public MatchInfoSnapshot info;

    private void Start()
    {
        //manager = NetworkManager.singleton;
        manager = PlayerSpawn.singleton;
        if (manager.matchMaker == null)
            manager.StartMatchMaker();
    }

    public void SetUp(MatchInfoSnapshot _info)
    {
        info = _info;
        nameTxt.text = info.name;
    }

    public void OnJointBtn()
    {
        manager.matchMaker.JoinMatch(info.networkId, "", "", "", 0, 0, manager.OnMatchJoined);


    }

}