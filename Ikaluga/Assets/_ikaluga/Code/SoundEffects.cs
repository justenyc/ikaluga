using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public SoundEffect[] soundEffects;
    Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

    private void Awake()
    {
        foreach (SoundEffect ac in soundEffects)
        {
            sounds.Add(ac.name, ac);
        }
    }

    public AudioClip GetClip(string name)
    {
        if (sounds[name].audioClip == null)
            Debug.LogError("Did not find audioClip in " + name);

        return sounds[name].audioClip;
    }
}

[System.Serializable]
public struct SoundEffect
{
    public string name;
    public AudioClip audioClip;
    [Range(0, 1)] public float volume;
    [Range(0, 3)] public float pitch;
}
