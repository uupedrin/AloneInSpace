using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Features list
    TODO Armazenar os Powerups
    TODO Gerenciar vida
    TODO Colisão com balas
*/

public class Player : MonoBehaviour
{
    public GameObject[] characters;
    public GameObject[] powerUps;
    private enum Special{ScreenBomb, LaserBeam, StarWay}



    //MOVEMENT

    //variables
    public int moveSpeed;
    float h,v;

    //functions
    void SetPlayer(){ //Set player sprite and move speed
        switch(GameController.controller.selectedCharacter){
            case PlayerCharacter.Bulldozer:
                moveSpeed = 5;
                __PlayerSetUp(0);
                break;
            case PlayerCharacter.Zapper:
                moveSpeed = 7;
                __PlayerSetUp(1);
                break;
            case PlayerCharacter.Mooncrest:
                moveSpeed = 10;
                __PlayerSetUp(2);
                break;
        }
    }

    void PlayerMovement(){ //Handle player movement
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 xMovement, yMovement;
        if((h < 0 && transform.position.x > -6.5) || ( h> 0 && transform.position.x < 6.5)){
            xMovement = Vector3.right * h;
        }else{
            xMovement = Vector3.zero;
        }
        if((v < 0 && transform.position.y > -3.3) || ( v> 0 && transform.position.y < 5.5)){
            yMovement = Vector3.up * v;
        }else{
            yMovement = Vector3.zero;
        }
        Vector3 movement = xMovement + yMovement;
        transform.position += movement.normalized * Time.deltaTime * moveSpeed;
    }



    //SHOOTING

    //variables
    public GameObject bullet;
    public Transform[] gunPositions = new Transform[3]; //0 == Arma 1 | 2 == Arma 2 | 3 == Powerup
    bool canFire = true;
    public float fireRate, bulletSpeed, bulletDestroyTime;
    //functions
    void ShootHandler(){
        if (Input.GetButton("Fire1") && canFire)
        {
            StartCoroutine("_Fire");
        }
    }



    //POWERUPS
    void PowerupHandler(){

    }



    //SUBFUNCTIONS
    void __PlayerSetUp(int spriteToEnable){

        for (int i = 0; i < characters.Length; i++)
        {
            if(i == spriteToEnable){
                characters[i].SetActive(true);
            }else{
                characters[i].SetActive(false);
            }
        }
    }

    IEnumerator _Fire()
    {
        canFire = false;
        for (int i = 0; i < 2; i++)
        {
            GameObject tempBullet = Instantiate(bullet, gunPositions[i].position, transform.rotation);
            tempBullet.tag = "PlayerBullet";
            Rigidbody tempRb = tempBullet.GetComponent<Rigidbody>();
            tempRb.AddForce(Vector3.up * bulletSpeed, ForceMode.VelocityChange);
            Destroy(tempBullet, bulletDestroyTime);
        }
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    public void __SetGunPositions(Transform[] guns){
        for (int i = 0; i < guns.Length; i++)
        {
            gunPositions[i] = guns[i];
        }
    }
        


    //Unity Functions
    private void Start() {
        GameController.controller.player = this;
        SetPlayer();
    }

    private void Update() {
        PlayerMovement();
        ShootHandler();
    }
}
