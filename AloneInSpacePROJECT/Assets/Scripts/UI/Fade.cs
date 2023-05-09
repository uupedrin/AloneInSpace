using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public void FadeScene(string scene){
        GameController.controller.uiController.ChangeScene(scene);
    }
}
