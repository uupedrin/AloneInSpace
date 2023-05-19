using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnRate;

    void Start()
    {
        InvokeRepeating("SpawnEnemies",spawnRate, spawnRate);
    }

    void SpawnEnemies(){
        int enemyToSpawn = Random.Range(0,2);
        float posX = Random.Range(-6,7);
        GameObject tempEnemy = Instantiate(enemies[enemyToSpawn], transform.position, transform.rotation);
        Vector3 pos = new Vector3( posX,transform.position.y, transform.position.z);
        transform.position = pos;
    }
}
