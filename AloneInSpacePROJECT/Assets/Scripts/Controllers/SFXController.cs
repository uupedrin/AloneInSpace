using UnityEngine;

public class SFXController : MonoBehaviour
{
    public static SFXController controller;
    public AudioClip[] sfxs;
    public AudioSource source;
    private void Awake() {
        if(controller == null){
            controller = this;
        }else{
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
    }
    private void Start() {
        source = GetComponent<AudioSource>();
    }
    public void PlaySFX(int sfxIndex){
        source.Stop();
        source.clip = sfxs[sfxIndex];
        source.Play();
    }
}
