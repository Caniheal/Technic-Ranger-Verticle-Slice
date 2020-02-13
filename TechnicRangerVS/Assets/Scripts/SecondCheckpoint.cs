using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondCheckpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
    PlayerController PC = other.gameObject.GetComponent<PlayerController>();

        if (PC)
        {
            PC.DisableMovement();
            GameManager.Instance.lastCheckPoint = transform;
            PC.EnableMovement();
        }
    }
}
