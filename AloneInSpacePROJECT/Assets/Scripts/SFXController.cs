using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    public AudioClip[] sfxs;
    public AudioSource source;
    
    private void Start() {
        source = GetComponent<AudioSource>();
    }
    public void PlaySFX(int sfxIndex){
        source.Stop();
        source.clip = sfxs[sfxIndex];
        source.Play();
    }
}
