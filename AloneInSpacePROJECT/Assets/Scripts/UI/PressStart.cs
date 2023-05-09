using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressStart : MonoBehaviour
{
    private Animator startAnimator;
    private void Start() {
        startAnimator = GetComponent<Animator>();
    }
    private void Update() {
        if(Input.GetAxisRaw("Submit")> 0){
            SFXController.controller.PlaySFX(2);
            startAnimator.SetTrigger("StartPressed");
        }
    }

    public void StartFinalAnimation(){
        MusicController.controller.StopSong();
        SFXController.controller.PlaySFX(5);
        GameController.controller.uiController.animator.SetTrigger("EndStart");
    }
}
