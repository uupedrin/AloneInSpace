using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public UIController uiController;
    public MusicController musicController;
    public SFXController sfxController;

    private void Start() {
        musicController.ChangeSong(4);
    }

    private void Update() {
        if(Input.GetAxisRaw("Submit")> 0){
            sfxController.PlaySFX(2);
            uiController.animator.SetTrigger("EndStart");
        }
    }
}
