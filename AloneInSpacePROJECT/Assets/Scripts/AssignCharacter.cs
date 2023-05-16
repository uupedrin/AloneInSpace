using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignCharacter : MonoBehaviour
{
    public Transform[] guns;
    void Start()
    {
        GameController.controller.player.__SetGunPositions(guns);
        GameController.controller.ToggleMouseCursor();
    }
}
