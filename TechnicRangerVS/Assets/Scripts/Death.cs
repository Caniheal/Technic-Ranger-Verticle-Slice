using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    
    public Transform startPos;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("player"))
        {
            other.gameObject.transform.position = startPos.position;
        }
    }
}
