using UnityEngine;
using UnityEngine.Audio;

public enum EnemySoundType
{
    guard_walk,
    guard_run,
    guard_alert,
    guard_attack
}

[RequireComponent(typeof(AudioSource))]
public class EnemySoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] soundsList;
    private static EnemySoundManager instance;


    void Awake()
    {
        if (instance == null)
        instance = this;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public static void PlayOSSound(EnemySoundType sound, float volume =1)
    {
        instance.audioSource.PlayOneShot(instance.soundsList[(int)sound], volume);
    }

    public static void StopSound(EnemySoundType sound)
    {
        instance.audioSource.Stop();
    }

}
