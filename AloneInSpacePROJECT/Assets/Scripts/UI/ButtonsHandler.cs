using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsHandler : MonoBehaviour
{
    //FIXME se o mouse clica fora ele para de funcionar nos buttons
    public Text[] uiTexts;
    public EventSystem eventSystem;

    private GameObject currentSelected;

    private void Start() {
        currentSelected = eventSystem.currentSelectedGameObject;
    }
    private void Update() {
        currentSelected = eventSystem.currentSelectedGameObject;

        switch(currentSelected.name){
            case "Character1":
                uiTexts[0].enabled = true;
                uiTexts[1].enabled = false;
                uiTexts[2].enabled = false;
                break;
            case "Character2":
                uiTexts[0].enabled = false;
                uiTexts[1].enabled = true;
                uiTexts[2].enabled = false;
                break;
            case "Character3":
                uiTexts[0].enabled = false;
                uiTexts[1].enabled = false;
                uiTexts[2].enabled = true;
                break;
        }
    }
}
