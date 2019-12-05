using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinKiller : MonoBehaviour
{
    public AudioSource sound;

    

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    

    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("workplz");

    
        
            sound.Play();


        

        Object.Destroy(gameObject);

     
            
        }
   
}

