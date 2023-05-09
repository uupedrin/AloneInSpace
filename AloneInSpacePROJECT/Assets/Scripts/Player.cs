using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] shipSprites;
    public int moveSpeed;
    public GameObject[] powerUps;
    public SpriteRenderer playerRednerer;
    private enum Special{ScreenBomb, LaserBeam, StarWay}

    float h,v;

    private void Start() {
        SetPlayer();
    }

    private void Update() {
        PlayerMovement();
    }

    void SetPlayer(){
        switch(GameController.controller.selectedCharacter){
            case PlayerCharacter.Bulldozer:
                _SetSprite(0);
                break;
            case PlayerCharacter.Zapper:
                _SetSprite(1);
                break;
            case PlayerCharacter.Mooncrest:
                _SetSprite(2);
                break;
        }
    }

    void _SetSprite(int spriteIndex){
        playerRednerer.sprite = shipSprites[spriteIndex];
    }

    void PlayerMovement(){
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
        Vector3 xMovement = Vector3.right * h;
        Vector3 yMovement = Vector3.up * v;
        Vector3 movement = xMovement + yMovement;
        transform.position += movement.normalized * Time.deltaTime * moveSpeed;
    }
}
