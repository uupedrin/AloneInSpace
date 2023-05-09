using UnityEngine;

public enum PlayerCharacter {Bulldozer, Zapper, Mooncrest};
public class GameController : MonoBehaviour
{
    public static GameController controller;
    public UIController uiController;
    
    public PlayerCharacter selectedCharacter;
    public int score, health, maxHealth;
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
                break;
            case PlayerCharacter.Zapper:
                health = 4;
                break;
            case PlayerCharacter.Mooncrest:
                health = 3;
                break;
        }
    }
}
