using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MainHUDAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonAudio;
    [SerializeField] private AudioClip countdownAudio;
    [SerializeField] private AudioClip countdownFinishedAudio;

    private AudioSource audioSource;

    private AudioSource AudioSource => audioSource == null ? audioSource = GetComponent<AudioSource>() : audioSource;

    public void PlayButtonAudio()
    {
        Play(buttonAudio);
    }

    public void PlayCountdownAudio()
    {
        Play(countdownAudio);
    }

    public void PlayCountdownFinishedAudio()
    {
        Play(countdownFinishedAudio);
    }

    private void Play(AudioClip clip)
    {
        AudioUtility.PlayAudioCue(AudioSource, clip);
    }
}
