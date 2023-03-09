using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterArea : MonoBehaviour
{
    public AudioSource audioSource;

    void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.tag == "Player" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider otherCollider) {
        if (otherCollider.tag == "Player" && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
