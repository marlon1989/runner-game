using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ObstacleDecoration : MonoBehaviour
{
    [SerializeField] private AudioClip collisionAudio;
    [SerializeField] private Animation collisionAnimation;

    private AudioSource audioSource;
    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayCollisionFeedback()
    {
        AudioUtility.PlayAudioCue(AudioSource, collisionAudio);

        if(collisionAnimation != null)
        {
            collisionAnimation.Play();
        }
    }
}
