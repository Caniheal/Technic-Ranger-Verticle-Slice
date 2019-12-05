using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinKiller : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip SoundToPlay;
    public float Volume;
    public bool alreadyPlayed = false;


    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
   


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("workplz");

        if (!alreadyPlayed)
        {
            sound.PlayOneShot(SoundToPlay, Volume);
            alreadyPlayed = true;
        }

        Object.Destroy(gameObject);

     
            
        }
   
}

