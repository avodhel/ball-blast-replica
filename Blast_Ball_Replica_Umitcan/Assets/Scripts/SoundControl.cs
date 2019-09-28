using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundControl : MonoBehaviour
{
    public SoundsList[] sounds; 

    void Awake() 
    {
        foreach (SoundsList s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void playSound(string _soundName)
    {
        SoundsList s = Array.Find(sounds, sound => sound.soundName == _soundName);
        if (s == null)
        {
            Debug.LogWarning(_soundName + " not found.");
            return;
        }
        s.source.Play();
    }
}
