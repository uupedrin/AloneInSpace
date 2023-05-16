using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(tag);
        if(tag == "PlayerBullet" && other.tag != "Player"){
            //Handle PlayerBullet collision

            
        }
        
        else if(tag == "EnemyBullet" && other.tag != "Enemy"){
            //Handle EnemyBullet collision

            
        }
    }
}
