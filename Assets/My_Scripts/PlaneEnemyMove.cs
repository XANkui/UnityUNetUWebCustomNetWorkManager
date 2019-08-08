using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneEnemyMove : MonoBehaviour {


    float MoveSpeed = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.World);
    }
}
