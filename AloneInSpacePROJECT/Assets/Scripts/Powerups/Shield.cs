using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    public float rotationSpeed, radius;
    public Transform player;
    void Update()
    {
        float a = Time.time * rotationSpeed;
        Vector3 pos = Vector3.zero;
        pos.x = player.position.x + Mathf.Cos(a) * radius;
        pos.y = player.position.y + Mathf.Sin(a) * radius;
        pos.z += 0.1f;
        transform.position = pos;

    }

    void OnTriggerEnter(Collider other)
    {
        switch(other.tag){
            case "EnemyBullet": Destroy(other.gameObject); break;
        }
    }
}
