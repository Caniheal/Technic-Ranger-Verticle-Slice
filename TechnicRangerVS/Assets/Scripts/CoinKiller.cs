using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinKiller : MonoBehaviour
{
    public AudioSource sound;
    public Collider coinBox;
    public GameObject diamond, ring;
    

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        coinBox.enabled = true;
    }

    

    void OnTriggerEnter(Collider other)
    {
        
        Debug.Log("workplz");
        if(diamond)
            diamond.GetComponent<MeshRenderer>().enabled = false;
        if(ring)
            ring.GetComponent<MeshRenderer>().enabled = false;
        if(!diamond && !ring)
            this.GetComponent<MeshRenderer>().enabled = false;

        coinBox.enabled = false;
            sound.Play();


        

        Object.Destroy(gameObject, .15f);

     
            
        }
   
}

