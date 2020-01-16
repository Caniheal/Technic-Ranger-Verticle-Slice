using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMasterVolume : MonoBehaviour
{

    public AudioMixer Master;

    public void SetVolume(float volume)
    {
        Master.SetFloat("MasterVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        Master.SetFloat("BGMVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        Master.SetFloat("SFXVolume", volume);
    }
}