using UnityEngine;

[CreateAssetMenu(fileName = "New Music", menuName = "ScriptableObjects/MusicObject", order = 1)]
public class MusicStruct : ScriptableObject
{
    public AudioClip audioFile;
    public bool loop;
}