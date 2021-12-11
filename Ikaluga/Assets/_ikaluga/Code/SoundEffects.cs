using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip[] soundEffects;
    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        foreach (AudioClip ac in soundEffects)
        {
            sounds.Add(ac.name, ac);
        }
    }

    public AudioClip GetClip(string name)
    {
        Debug.Log(name);
        return sounds[name];
    }
}
