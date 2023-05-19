using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Features list
    TODO Gerenciar vida
    TODO Colisão com balas
    TODO Organizar o código dos powerups e programar seus efeitos
*/

public class Player : MonoBehaviour
{
    public GameObject[] characters;
    private enum POWERUPS{SCREENBOMB, LASERBEAM, STARWAY, HEALTHGAIN, MISSILE, FIRERATEUP, SHIELD, NONE}

    SpriteRenderer selfSprite;

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
        GameController.controller.SetPlayerCharacter(GameController.controller.selectedCharacter);
        GameController.controller.ResetScoreAndLife();
        GameController.controller.uiController.SetHealthValue(GameController.controller.MAXHEALTH);
        GameController.controller.uiController.SetScoreValue(0);
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
    public GameObject[] bullets = new GameObject[2];
    public Transform[] gunPositions = new Transform[3]; //0 == Arma 1 | 2 == Arma 2 | 3 == Powerup
    bool canFire = true;
    public float fireRate, bulletSpeed, bulletDestroyTime;
    float OGFIRERATE;
    int currentBullet = 0;

    //functions
    void ShootHandler(){
        if (Input.GetButton("Fire1") && canFire)
        {
            StartCoroutine("_Fire");
        }
    }



    //SPECIALS
    //variables
    POWERUPS currentSpecial;
    public float specialRate;
    float specialAmount = 0;
    bool barFilled = false;

    //functions
    void SpecialHandler(){
        if(specialAmount<100 && !barFilled){
            specialAmount+= 1 * specialRate * Time.deltaTime;
            _UpdateSpecialUI(specialAmount);
        }else if (specialAmount >= 100 && !barFilled){
            specialAmount = 100;
            _UpdateSpecialUI(specialAmount);
            barFilled = true;
        }

        if(Input.GetButtonUp("GetSpecial") && barFilled){

            specialAmount = 0;
            _UpdateSpecialUI(specialAmount);
            _SetPowerup((int)currentSpecial);
            barFilled = false;
        }
    }



    //POWERUPS
    //variables
    POWERUPS[] currentPwps = {POWERUPS.NONE, POWERUPS.NONE};
    POWERUPS powerupInEffect;


    //functions
    void PowerupUseHandler(){
        if(Input.GetButtonDown("UsePowerup") && currentPwps[0] != POWERUPS.NONE){
            DisableCurrentPowerup();
            switch(currentPwps[0]){
                case POWERUPS.SCREENBOMB: powerupInEffect = POWERUPS.SCREENBOMB; ScreenBomb(); break;
                case POWERUPS.LASERBEAM: powerupInEffect = POWERUPS.LASERBEAM; LaserBeam(); break;
                case POWERUPS.STARWAY: powerupInEffect = POWERUPS.STARWAY; StartCoroutine("StarWay"); break;
                case POWERUPS.HEALTHGAIN: powerupInEffect = POWERUPS.HEALTHGAIN; HealthGain(); break;
                case POWERUPS.MISSILE: powerupInEffect = POWERUPS.MISSILE; Missile(); break;
                case POWERUPS.FIRERATEUP: powerupInEffect = POWERUPS.FIRERATEUP; FireRateUp(); break;
                case POWERUPS.SHIELD: powerupInEffect = POWERUPS.SHIELD; Shield(); break;
            }
            _UsePowerup();           
        }
    }

    void DisableCurrentPowerup(){
        switch(powerupInEffect){
            case POWERUPS.LASERBEAM: LaserBeam(); break;
            case POWERUPS.HEALTHGAIN: HealthGain(); break;
            case POWERUPS.MISSILE: Missile(); break;
            case POWERUPS.FIRERATEUP: FireRateUp(); break;
            case POWERUPS.SHIELD: Shield(); break;
        }
    }


    //Powerups Variables
    bool screenBombIsActive, laserBeamIsActive, starWayIsActive, healthGainIsActive, missileIsActive, fireRateisActive, shieldIsActive;
    
    public ScreenBomb screenBombArea;
    
    void ScreenBomb(){
        screenBombIsActive = !screenBombIsActive;
        if(screenBombIsActive){
            screenBombArea.screenBombActive = screenBombIsActive;
            screenBombIsActive = false;
            screenBombArea.screenBombActive = screenBombIsActive;
        }
    }
    void LaserBeam(){
        laserBeamIsActive = !laserBeamIsActive;

    }
    IEnumerator StarWay(){
        starWayIsActive = !starWayIsActive;
        GameController.controller.starWayActive = starWayIsActive;
        if(starWayIsActive){
            _ChangeColor();
            yield return new WaitForSeconds(10);
            _NormalColor();
            starWayIsActive = false;
            GameController.controller.starWayActive = starWayIsActive;
        }

    }
    void HealthGain(){
        healthGainIsActive = !healthGainIsActive;
        if(healthGainIsActive){
            GameController.controller.IncreasePlayerHealth();
            Invoke("_Blink", 1f);
        }
    }

    void Missile(){
        missileIsActive = !missileIsActive;
        if(missileIsActive){
            currentBullet = 1;
            fireRate *= 2;
        }else{
            currentBullet = 0;
            fireRate = OGFIRERATE;
        }

    }

    public float FireRatePWPValue;
    void FireRateUp(){
        fireRateisActive = !fireRateisActive;
        if(fireRateisActive){
            fireRate = FireRatePWPValue;
        }else{
            fireRate = OGFIRERATE;
        }

    }

    public GameObject shield;
    void Shield(){
        shieldIsActive = !shieldIsActive;
        if(shieldIsActive){
            shield.SetActive(true);
        }else{
            shield.SetActive(false);
        }
    }



    //SUBFUNCTIONS
    void __PlayerSetUp(int spriteToEnable){

        for (int i = 0; i < characters.Length; i++)
        {
            if(i == spriteToEnable){
                currentSpecial = (POWERUPS)i;
                characters[i].SetActive(true);
                selfSprite = characters[i].GetComponentInChildren<SpriteRenderer>();
            }else{
                characters[i].SetActive(false);
            }
        }
    }

    IEnumerator _Fire()
    {
        canFire = false;
        if(currentBullet == 0){
            for (int i = 0; i < 2; i++)
            {
                GameObject tempBullet = Instantiate(bullets[currentBullet], gunPositions[i].position, transform.rotation);
                tempBullet.tag = "PlayerBullet";
                Rigidbody tempRb = tempBullet.GetComponent<Rigidbody>();
                tempRb.AddForce(Vector3.up * bulletSpeed, ForceMode.VelocityChange);
                Destroy(tempBullet, bulletDestroyTime);
            }
        }
        else{
            GameObject tempBullet = Instantiate(bullets[currentBullet], gunPositions[2].position, transform.rotation);
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

    void _UpdateSpecialUI(float specialFilling){
        GameController.controller.uiController.increaseSpecialBar(specialFilling);
    }
    void _UpdatePowerupUI(){
        for (int i = 0; i < currentPwps.Length; i++)
        {
            GameController.controller.uiController.SetPowerupImages(i, (int)currentPwps[i]);
        }
    }
    public void _SetPowerup(int powerUpIndex){
        if(currentPwps[0] == POWERUPS.NONE){
            currentPwps[0] = (POWERUPS)powerUpIndex;
        }else if(currentPwps[1] != POWERUPS.NONE){
            currentPwps[0] = currentPwps[1];
            currentPwps[1] = (POWERUPS)powerUpIndex;
        }else{
            currentPwps[1] = (POWERUPS)powerUpIndex;
        }
        for (int i = 0; i < currentPwps.Length; i++)
        {
            _UpdatePowerupUI();
        }
    }
    public void _UsePowerup(){
        currentPwps[0] = currentPwps[1];
        currentPwps[1] = POWERUPS.NONE;
        for (int i = 0; i < currentPwps.Length; i++)
        {
            _UpdatePowerupUI();
        }
    }
    
    public void _ChangeColor(){
        selfSprite.color = new Color (0,255,238,255);
    }
    public void _NormalColor(){
        selfSprite.color = new Color (255,255,255,255);
    }



    //Unity Functions
    private void Start() {
        GameController.controller.player = this;
        OGFIRERATE = fireRate;
        SetPlayer();
    }

    private void Update() {
        PlayerMovement();
        ShootHandler();
        SpecialHandler();
        PowerupUseHandler();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.gameObject.CompareTag("Powerup")){
            GameObject powerup = collisionInfo.gameObject;
            _SetPowerup(powerup.GetComponent<Powerups>().powerupIndex);
            Destroy(collisionInfo.gameObject);
        }
    }
}
