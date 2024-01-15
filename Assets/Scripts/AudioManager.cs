using System;
using UnityEngine;

/// <summary>
/// Handles audio loading and playing.
/// </summary>
public class AudioManager : MonoBehaviour
{
    /// <summary>
    /// Array of sounds linked to the audio manager object in editor.
    /// </summary>
    public Sound[] sounds;

    void Awake()
    {
        // TODO: These resources are static yet they will be called on every new scene. Check if this can be optimized.
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    /// <summary>
    /// Stop playing the specified sound.
    /// </summary>
    /// <param name="name">Sound to stop playing.</param>
    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound {name} not found");
        }
        else
        {
            sound.source.Stop();
        }
    }

    /// <summary>
    /// Start playing the specified sound.
    /// </summary>
    /// <param name="name">Sound to start playing.</param>
    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound {name} not found");
        }
        else
        {
            sound.source.Play();
        }
    }

    /// <summary>
    /// Start playing the specified sound after the specified delay.
    /// </summary>
    /// <param name="name">Sound to start playing.</param>
    /// <param name="delay">Delay to start playing in seconds.</param>
    public void PlayDelayed(string name, float delay)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning($"Sound {name} not found");
        }
        else
        {
            sound.source.PlayDelayed(delay);
        }
    }

}
