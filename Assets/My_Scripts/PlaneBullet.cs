using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBullet : MonoBehaviour {

    public GameObject TX;
    [SerializeField]
    private int damage = 100;
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("OnTriggerEnter");
        GameObject hit = col.gameObject;
        Health health = hit.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(this.gameObject);

        GameObject tmp = Instantiate(TX, this.transform.position, Quaternion.identity);
        Destroy(tmp,5);
    }

    float MoveSpeed = 10;
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime, Space.Self);
    }
}
