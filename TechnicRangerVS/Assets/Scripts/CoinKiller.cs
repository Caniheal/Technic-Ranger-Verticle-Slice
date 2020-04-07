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


        Object.Destroy(gameObject, .15f);

     
            
        }
    void SetCountText()
    {
        countText.text = "Coins Collected: " + count.ToString();
        if (count >= 12)
        {
            
        }
    }

}

