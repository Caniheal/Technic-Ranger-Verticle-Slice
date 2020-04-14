using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinKiller : MonoBehaviour
{
    public AudioSource sound;
    public Collider coinBox;
    public GameObject diamond, ring;
    public Text countText;
    private int count;

    public ParticleSystem pickUpEffect;
    public ParticleSystem orbEffect;


    private void Start()
    {
        sound = GetComponent<AudioSource>();
        coinBox.enabled = true;
        count = 0;
        SetCountText();
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

            count = count + 1;
            SetCountText();


        Destroy();

     
            
        }
    void SetCountText()
    {
        countText.text = "Coins Collected: " + count.ToString();
        
    }
    public void Destroy()
    {
        Instantiate(pickUpEffect, transform.position, Quaternion.identity);
        Instantiate(orbEffect, transform.position, Quaternion.identity);

        Object.Destroy(gameObject, .15f);
    }

}

