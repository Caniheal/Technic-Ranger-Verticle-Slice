using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;

    public float hoverF = 12;

    void OnTriggerEnter(Collider other)
    {
        player.transform.position = startPoint.transform.position;
  
        Debug.Log("working");
    }
}
