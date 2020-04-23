using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public Transform startPos;
    private AudioSource source;
    public AudioClip deathClip; //this is bugged by the way
    //public GameObject Player;

    private void OnTriggerEnter(Collider other)
    {
        //Player.GetComponent<PlayerController>().SpawnAtCheckpoint();

        PlayerController PC = other.gameObject.GetComponent<PlayerController>();

        if (PC)
        {
            PC.DisableMovement();
            PC.transform.position = GameManager.Instance.lastCheckPoint.position;
            //deathclip is bugged, uncomment with caution
            //source.PlayOneShot(deathClip);

            PC.EnableMovement();
        }

    }
}