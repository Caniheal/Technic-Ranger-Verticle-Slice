using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public Transform startPos;
    private AudioSource source;
    public AudioClip deathClip;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController PC = other.gameObject.GetComponent<PlayerController>();

        if (PC)
        {
            PC.DisableMovement();
            other.gameObject.transform.position = GameManager.Instance.lastCheckPoint.position;
            source.PlayOneShot(deathClip);

            PC.EnableMovement();
        }

    }
}