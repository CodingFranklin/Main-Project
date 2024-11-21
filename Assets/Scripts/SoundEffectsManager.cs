using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [SerializeField] AudioSource soundObject;


    private void Awake() 
    {
        instance = this;
    }

    public void PlaySoundEffectClip(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);
    }
}
