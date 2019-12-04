﻿using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Transform checkpoint;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter (Collider player)
    {
        if(player.gameObject.tag == "Player")
        {
            player.transform.position = checkpoint.position;
            player.transform.rotation = checkpoint.rotation;
        }
    }
}
