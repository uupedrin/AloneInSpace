using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(tag == "PlayerBullet" && other.CompareTag("Enemy")){
            //Handle PlayerBullet collision

            
        }
        
        else if(tag == "EnemyBullet" && other.CompareTag("Player")){
            //Handle EnemyBullet collision

        }
    }
}
