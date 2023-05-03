using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController uiController;
    
    public int score, health;
    private void Awake() {
        if(controller == null){
            controller = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
