using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum MovementPatterns
    {
        SINWAVE,
        ARCH 
    }
    public MovementPatterns movement;


    public float moveSpeed, powerUpDropChance, fireRate;
    public int health;
    public GameObject powerup, bullet;
    public Transform[] gunPositions;
    public bool canBeKilled = false, startShooting = false;

    float sinCenterX;
    public float bulletSpeed, bulletDestroyTime= 2, sinWaveAmplitude = 1, sinWaveFrequency = 1;
    public bool mirrorSinWave = false;

    void Start(){
        sinCenterX = transform.position.x;
        archCenterX = transform.position.x;
        if(mirrorArch) radius *=  -1;
    }
    void Update()
    {
        switch(movement){
            case MovementPatterns.SINWAVE:
                SinWaveMovement();
            break;
            case MovementPatterns.ARCH:
                ArchMovement();
            break;
        }
        if(transform.position.y <= 6 && !canBeKilled){
            canBeKilled = true;
            InvokeRepeating("Shoot", fireRate, fireRate);
        }
    }


    void Shoot(){
        for (int i = 0; i < gunPositions.Length; i++)
        {
            GameObject tempBullet = Instantiate(bullet, gunPositions[i].position, gunPositions[i].rotation);
            tempBullet.tag = "EnemyBullet";
            Rigidbody tempRb = tempBullet.GetComponent<Rigidbody>();
            tempRb.AddForce(Vector3.down * bulletSpeed, ForceMode.VelocityChange);
            Destroy(tempBullet, bulletDestroyTime);
        }
    }


    void MoveDown(){
        transform.position += Vector3.down * Time.deltaTime * moveSpeed;
    }

    Vector3 returnMoveDown(){
        return Vector3.down * Time.deltaTime * moveSpeed;
    }

    void SinWaveMovement(){
        MoveDown();
        Vector3 pos = transform.position;
        float sin = Mathf.Sin(pos.y * sinWaveFrequency) * sinWaveAmplitude;

        if(mirrorSinWave) sin *= -1;

        pos.x = sinCenterX + sin;

        transform.position = pos;
    }

    public float rotationSpeed, radius;
    public bool gotInArch = false, mirrorArch;

    float archCenterX, OGTransformY;
    
    void ArchMovement(){
        MoveDown();
        if(transform.position.y <= 9.5 && !gotInArch){
            OGTransformY = transform.position.y;
            gotInArch = true;
        }
        if(gotInArch){
            float a = Time.time * rotationSpeed;
            Vector3 pos = Vector3.zero;
            pos.x = archCenterX + Mathf.Cos(a) * radius * -1;
            OGTransformY -= Time.deltaTime * 0.5f;
            pos.y = OGTransformY + Mathf.Sin(a) * radius;
            transform.position = pos;
        }
    }

    public int pwpSpawnPercentage = 30;
    public void ReduceHealth(int amount = 1){
        health-= amount;
        if (health <= 0){
            int spawnPWP = Random.Range(0,101);
            if (spawnPWP <= pwpSpawnPercentage){
                Instantiate(powerup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !GameController.controller.starWayActive){
            GameController.controller.ReducePlayerHealth();
            ReduceHealth(2);
        }
    }

    
    void OnDestroy()
    {
        GameController.controller.increasePlayerScore(250);
    }
}
