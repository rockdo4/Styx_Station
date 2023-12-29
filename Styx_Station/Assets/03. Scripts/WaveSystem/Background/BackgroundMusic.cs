using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    public AudioSource m_AudioSource;

    public void SetAudioClip(int index)
    {
        m_AudioSource.Stop();
        m_AudioSource.clip = audioClips[index];
        PlayAudio();
    }

    public void PlayAudio()
    {
        m_AudioSource.Play();
    }

    public void StopAudio()
    {
        m_AudioSource.Stop();
    }
}
