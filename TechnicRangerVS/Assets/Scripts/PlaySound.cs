using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
   
    public AudioSource sound;
    public bool alreadyPlayed = false;
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("workplz");



        sound.Play();
    }
}
