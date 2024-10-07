using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    //public AudioSource audioSource;
    //public void ChangeAudio(float volume)
    //{
    //    //musicAudioSource.volume = volume;
    //    audioSource.volume = volume;
    //}

    public Text masterDisplay;
    public AudioMixer audioMixer;
    [SerializeField] string audioMixerChannel;
    public void CurrentMixer(string name)
    {
        audioMixerChannel = name;
    }

    public void changeVolume(float volume)
    { 
        audioMixer.SetFloat(audioMixerChannel, volume);
        ChangeText(name, volume);
    }

    public void ChangeText(string name, float volume)
    {
        masterDisplay.text = $"{Mathf.Clamp01((volume + 80) / 100):P0}";
    }

    public void GetText(Text UiText)
    { 
        masterDisplay = UiText;
    }
}
