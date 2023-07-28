using UnityEngine;

[System.Serializable]
public class AudioSound {
    
    public string name;
    public AudioClip clip;
    [HideInInspector]
    public AudioSource source;

    public void InitializeSound(AudioSource source) {
        this.source = source;
        this.source.clip = clip;
    }
}
