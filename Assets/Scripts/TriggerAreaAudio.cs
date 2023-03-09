using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume;

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Player" && !audioSource.isPlaying)
        {
            StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, maxVolume));
        }
    }

    void OnTriggerExit(Collider otherCollider) {
        if (otherCollider.tag == "Player" && audioSource.isPlaying)
        {
            StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, 0));
        }
    }
}
