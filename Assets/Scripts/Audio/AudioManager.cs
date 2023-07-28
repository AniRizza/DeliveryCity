using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSound[] sounds;

    // Start is called before the first frame update
    void Awake() {
        foreach (var sound in sounds)
        {
            sound.InitializeSound(gameObject.AddComponent<AudioSource>());
        }
    }

    public void PlaySound(string name) {
        Array.Find(sounds, sound => sound.name == name).source.Play();
    }
}
