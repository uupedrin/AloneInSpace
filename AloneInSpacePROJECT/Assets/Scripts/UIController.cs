using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Animator animator;
    public Canvas canvas;
    private void Start() {
        GameController.controller.uiController = this;
    }

    public void ChangeScene(string sceneToGo){
        SceneManager.LoadScene(sceneToGo);
    }

    public void SetTrigger(string trigger){
        animator.SetTrigger(trigger);
    }
}
