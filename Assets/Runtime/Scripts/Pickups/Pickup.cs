using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickup : MonoBehaviour
{
    [SerializeField] private AudioClip pickupAudio;

    [SerializeField] private GameObject model;

    public void OnPickedUp()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        AudioUtility.PlayAudioCue(audioSource, pickupAudio);

        model.SetActive(false);
        Destroy(gameObject, pickupAudio.length);
    }
}
