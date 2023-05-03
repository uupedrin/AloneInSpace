using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public MusicStruct[] musics;
    public AudioSource source;

    private int nextSongIdx = -1;
    
    private void Start() {
        source = GetComponent<AudioSource>();
    }
    public void ChangeSong(int songIndex, int nextSongIndex = -1){
        source.Stop();
        source.clip = musics[songIndex].audioFile;
        source.loop = musics[songIndex].audioFile;
        source.Play();
        if(nextSongIndex>=0){
            nextSongIdx = nextSongIndex;
            Invoke("nextSong", source.clip.length);
        }
    }
    private void nextSong(){
        ChangeSong(nextSongIdx);
    }
}
