using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerPlaneController : NetworkBehaviour
{

    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public GameObject playerCamera;

    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }


        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up * h * 120 * Time.deltaTime);
        transform.Translate(Vector3.up * v * 3 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
    }


    public override void OnStartLocalPlayer()
    {
        //这个方法只会在本地角色那里调用  当角色被创建的时候
        GameObject.Find("StarSparrow1").GetComponent<MeshRenderer>().material.color = Color.white;
        playerCamera.SetActive(true);   // 只激活自己的camera
        Debug.Log("PlayerPlaneController OnStartLocalPlayer");

        PlaneEnemySpawn.Instance.FindPlayerNumber();
    }


    [Command]// called in client, run in server
    void CmdFire()//这个方法需要在server里面调用
    {
        // 子弹的生成 需要server完成，然后把子弹同步到各个client
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
        Destroy(bullet, 2);

        NetworkServer.Spawn(bullet);
    }
}
