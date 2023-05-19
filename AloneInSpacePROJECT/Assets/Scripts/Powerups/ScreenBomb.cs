using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBomb : MonoBehaviour
{
    public bool screenBombActive;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Enemy") && screenBombActive){
            Destroy(other.gameObject);
        }
    }
}
