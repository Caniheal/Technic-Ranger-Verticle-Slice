using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinKiller : MonoBehaviour
{
    public AudioSource sound;
    public Collider coinBox;
    public GameObject diamond, ring;

    public ParticleSystem pickUpEffect;
    public ParticleSystem orbEffect;


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
            sound.Play(0);


        Destroy();

     
            
    }
    
    public void Destroy()
    {
        Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        Instantiate(orbEffect, transform.position, Quaternion.identity);

        Object.Destroy(gameObject,.15f);
    }

}

