using UnityEngine;
using System;
using UnityEngine.Audio;

public enum SoundType
{
    WALK,
    RUN,
    DASH,
    JUMP,
    LAND,
    STUN, 
    HURT,
    TIMEJUMP,
    PRESS,
    HOVER,
    ZAP,
    SCAN
}


[RequireComponent(typeof(AudioSource))] 
public class SoundEffectsOSManager : MonoBehaviour
{
    
    [SerializeField] private AudioClip[] soundList;
    private static SoundEffectsOSManager instance;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] loopSounds;


    void Awake()
    {
        if (instance == null)
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public static void PlayOSSound(SoundType sound, float volume =1)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void StopSound(SoundType sound)
    {
        instance.audioSource.Stop();
    }

    public static void PlaySound(SoundType sound)
    {
        instance.audioSource.clip = instance.soundList[(int)sound];
        instance.audioSource.loop = true;
        instance.audioSource.Play();
    }
}
