using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Animator animator;
    public Animator fadeAnimator;
    public int audioToPlay;
    private PlayerCharacter selectedCharacter;
    private void Start() {
        GameController.controller.uiController = this;
        MusicController.controller.ChangeSong(audioToPlay);
    }

    public void ChangeScene(string sceneToGo){
        SceneManager.LoadScene(sceneToGo);
    }

    public void SetTrigger(string trigger){
        animator.SetTrigger(trigger);
    }

    public void CallFadeOut(){
        fadeAnimator.SetTrigger("FadeOut");
    }

    public void CallSFX(int sfxToCall){
        SFXController.controller.PlaySFX(sfxToCall);
    }
    public void SetCharacter(int character){
        switch(character){
            case 0:
                selectedCharacter = PlayerCharacter.Bulldozer;
            break;
            case 1:
                selectedCharacter = PlayerCharacter.Zapper;
            break;
            case 2:
                selectedCharacter = PlayerCharacter.Mooncrest;
            break;
        }
        GameController.controller.SetPlayerCharacter(selectedCharacter);
    }
}
