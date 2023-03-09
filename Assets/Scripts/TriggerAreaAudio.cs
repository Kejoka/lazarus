using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaAudio : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Player" && !audioSource.isPlaying)
        {
            StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, 1));
        }
    }

    void OnTriggerExit(Collider otherCollider) {
        if (otherCollider.tag == "Player" && audioSource.isPlaying)
        {
            StartCoroutine(FadeAudioSource.StartFade(audioSource, 3, 0));
        }
    }
}
