using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    // [SerializeField] private Transform player;
    // [SerializeField] private Transform startPoint;

    public GameObject player;
    public GameObject lefty;
    public GameObject righty;

    void OnTriggerEnter(Collider other)
    {
        // player.transform.position = new Vector3(0,0,0);

        player.GetComponent<SideMover>().enabled = true;

        lefty.GetComponent<Light>().enabled = true;

        righty.GetComponent<Light>().enabled = true;

        Debug.Log("working");
    }
}
