using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlaneEnemySpawn : NetworkBehaviour
{

    public GameObject enemyPrefab;
    public GameObject NewBackButton;
    public Button StartGameButton;

    private static PlaneEnemySpawn instance;

    private int playerNumber = 0;

    public static PlaneEnemySpawn Instance { get {
            return instance;
        } }

    //[SyncVar(hook = "OnTimeEvent")]
    public int tmieConuter = 0;
    //private bool isStartCouter = false;

    void Awake() {
        instance = this;
    }

    void FixedUpdate()
    {
        //Debug.Log("FixedUpdate time :" + Time.deltaTime);

    }


    

    public override void OnStartServer()
    {
        TipShow("等待玩家加入，您可以漫游下场景，祝你愉快...");
        //StartCoroutine(EnemySpawnQueue());
        //isStartCouter = true;

        // 监控新玩家进入
        InvokeRepeating("FindPlayerNumber", 1,1);

        Debug.Log("OnStartServer==================");
    }

    public void FindPlayerNumber()
    {
        
        playerNumber = GameObject.FindGameObjectsWithTag("Player").Length;
        if (playerNumber >= 2)
        {
            // 取消监控新玩家进入
            CancelInvoke("FindPlayerNumber");

            tmieConuter = 2;

            RpcBackButtonHide();

            // 房主显示开始游戏按钮
            if(isServer == true)
            {
                StartGameButton.gameObject.SetActive(true);
                RpcTipShow("等待房主开始游戏...");
            }

            
        }

        Debug.Log("playerNumber:" + playerNumber);
        
    }




    /// <summary>
    /// 开始生成敌机，开始游戏
    /// </summary>
    public void CmdGameStart() {
        //开始游戏按钮隐藏
        StartGameButton.gameObject.SetActive(false);
        StartCoroutine(EnemySpawnQueue());

    }

    IEnumerator EnemySpawnQueue() {
        RpcTipShow("游戏开始，3s 后，第一波来袭...");
        yield return new WaitForSeconds(3.0f);
        //tmieConuter = 1;
        RpcTipHide();
        Coroutine firstSpawn = StartCoroutine(FirstEnemySpawn());
        yield return firstSpawn;
        yield return new WaitForSeconds(5.0f);
        //tmieConuter = 2;
        RpcTipShow("5s 后，下一波来袭...");
        yield return new WaitForSeconds(5.0f);
        //tmieConuter = 1;
        RpcTipHide();
        Coroutine secondSpawn = StartCoroutine(SecondEnemySpawn());
        yield return secondSpawn;

        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();

        firstSpawn = StartCoroutine(FirstEnemySpawn());
        yield return firstSpawn;
        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();

        secondSpawn = StartCoroutine(SecondEnemySpawn());
        yield return secondSpawn;
        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();

        firstSpawn = StartCoroutine(FirstEnemySpawn());
        yield return firstSpawn;
        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();

        secondSpawn = StartCoroutine(SecondEnemySpawn());
        yield return secondSpawn;
        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();

        firstSpawn = StartCoroutine(FirstEnemySpawn());
        yield return firstSpawn;
        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();

        secondSpawn = StartCoroutine(SecondEnemySpawn());
        yield return secondSpawn;
        RpcTipShow("10s 后，下一波来袭...");
        yield return new WaitForSeconds(10.0f);
        RpcTipHide();


        Coroutine thirdSpawn = StartCoroutine(ThirdEnemySpawn());
        yield return thirdSpawn;
        yield return new WaitForSeconds(10.0f);

        StartCoroutine(GameFinish("游戏结束，3s后自动退出"));
    }

    IEnumerator FirstEnemySpawn() {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 5.0f; i++) {


            PlaneSpawn();

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SecondEnemySpawn()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 10.0f; i++)
        {
            PlaneSpawn();

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ThirdEnemySpawn()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < 15.0f; i++)
        {

            PlaneSpawn();

            yield return new WaitForSeconds(0.35f);
        }
    }


    IEnumerator GameFinish(string msg) {
        yield return new WaitForEndOfFrame();
        //tmieConuter = 3;
        RpcTipShow(msg);
        yield return new WaitForSeconds(3);
        NetworkManager.singleton.StopHost();
    }

    void OnTimeEvent(int time) {
        switch (time) {
            case 1:
                
                break;

            case 2:
                CmdGameStart();

                break;

            case 3:
               
                break;
        }
    }

    void TipShow(string msg){
        GameObject.Find("GameFinishText").GetComponent<Text>().enabled = true;
        GameObject.Find("GameFinishText").GetComponent<Text>().text = msg;
    }

    //[ClientRpc]
    void RpcBackButtonHide() {
        NewBackButton.SetActive(false);
        
    }



    [ClientRpc]
    void RpcTipShow(string msg)
    {
        TipShow(msg);
    }

    [ClientRpc]
    void RpcTipHide()
    {
        GameObject.Find("GameFinishText").GetComponent<Text>().enabled = false;
    }

    private void PlaneSpawn() {
        Vector3 position = PlaneSpawnRandomPosition();

        Quaternion rotation = Quaternion.Euler(0, 180, 0);

        GameObject enemy = Instantiate(enemyPrefab, position, rotation, this.transform) as GameObject;
        enemy.GetComponent<Rigidbody>().velocity = enemy.transform.forward * Random.Range(2.0f, 6.0f);
        Destroy(enemy, 10);
        NetworkServer.Spawn(enemy);
    }

    private Vector3 PlaneSpawnRandomPosition() {

        return new Vector3(Random.Range(-0.50f, 2.5f), Random.Range(-2.5f, 2.5f),10);

    }
}
