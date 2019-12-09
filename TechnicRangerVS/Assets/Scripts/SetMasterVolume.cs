﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetMasterVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetVolume(float MasterVolume)
    {
        mixer.SetFloat("MasterVolume", MasterVolume);
    }
}