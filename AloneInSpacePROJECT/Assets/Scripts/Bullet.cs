using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1, amountOfHits = 1;
    int hits = 1;
    void OnTriggerEnter(Collider other)
    {
        if(tag == "PlayerBullet" && other.CompareTag("Enemy")){
            //Handle PlayerBullet collision
            GameObject enemy = other.gameObject; 
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if(enemyScript.canBeKilled){
                enemyScript.ReduceHealth(damage);
            }
            if(damage > 1){
                hits++;
                if(hits >= amountOfHits){
                    Destroy(gameObject);
                }
            }
            
        }
        
        else if(tag == "EnemyBullet" && other.CompareTag("Player")){
            //Handle EnemyBullet collision
            if(!GameController.controller.starWayActive){//Recebe dano e destroi a bala
                GameController.controller.ReducePlayerHealth(damage);
                Destroy(gameObject);
            }
        }
    }
}
