using UnityEngine;

public enum PlayerCharacter {Bulldozer, Zapper, Mooncrest};
public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController uiController;
    public Player player;

    public PlayerCharacter selectedCharacter;
    public int score, health, MAXHEALTH;

    public bool starWayActive = false;



    private void Awake() {
        if(controller == null){
            controller = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerCharacter(PlayerCharacter character){
        selectedCharacter = character;
        switch(character){
            case PlayerCharacter.Bulldozer:
                health = 5;
                MAXHEALTH = 5;
                break;
            case PlayerCharacter.Zapper:
                health = 4;
                MAXHEALTH = 4;
                break;
            case PlayerCharacter.Mooncrest:
                health = 3;
                MAXHEALTH = 3;
                break;
        }
    }

    public void ToggleMouseCursor(){
        if(Cursor.visible){
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }else{
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void ReducePlayerHealth(int amount=1){
        health-= amount;
        if(health < 0){
            ToggleMouseCursor();
            uiController.ChangeScene("Defeat");
        }
        uiController.SetHealthValue(health);
    }

    public void IncreasePlayerHealth(int amount = 1){
        health+= amount;
        if (health > MAXHEALTH){
            health = MAXHEALTH;
        }
        uiController.SetHealthValue(health);
    }

    public void ResetScoreAndLife(){
        score = 0;
        health = MAXHEALTH;
    }

    public void increasePlayerScore(int amount){
        score+= amount;
        uiController.SetScoreValue(score);
        if(score>= 5000){
            ToggleMouseCursor();
            uiController.ChangeScene("Victory");
        }
    }
}
