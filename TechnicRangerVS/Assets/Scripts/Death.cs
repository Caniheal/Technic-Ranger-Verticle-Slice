using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    public Transform startPos;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController PC = other.gameObject.GetComponent<PlayerController>();

        if (PC)
        {
            PC.DisableMovement();
            other.gameObject.transform.position = startPos.position;
            PC.EnableMovement();
        }

    }
}