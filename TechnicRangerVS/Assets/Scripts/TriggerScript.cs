using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public SideMover Object; 

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Checks if character (that has the playercontroller attached) is touching trigger
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        //Checks if player is null/exists
        if (player != null)
        {
            Object.enabled = true;
        }

    }
}


