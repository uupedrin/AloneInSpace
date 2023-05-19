using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Powerups : MonoBehaviour
{
    public int powerupIndex = -1;
    public Sprite[] powerUpImages;

    public float dropSpeed;
    SpriteRenderer selfSprite;

    void Start()
    {
        int rand = Random.Range(3,7);
        if(powerupIndex < 0) powerupIndex = rand;
        selfSprite = GetComponent<SpriteRenderer>();
        selfSprite.sprite = powerUpImages[powerupIndex];
        InvokeRepeating("Blink", .5f, 1f);
    }

    
    public void _NormalColor(){
        selfSprite.color = new Color (255,255,255,255);
    }

    void Update(){
        MoveDown();
    }

    void Blink(){
        selfSprite.color = new Color(255,255,255,0);
        Invoke("_NormalColor", .3f);
    }

    void MoveDown(){
        transform.position += Vector3.down * Time.deltaTime * dropSpeed;
    }

}
