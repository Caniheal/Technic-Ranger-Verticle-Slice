using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondCheckpoint : MonoBehaviour
{
    public AudioSource sound;

    private void OnTriggerEnter(Collider other)
    {
    PlayerController PC = other.gameObject.GetComponent<PlayerController>();

        if (PC)
        {
            PC.DisableMovement();
            GameManager.Instance.lastCheckPoint = transform;
            PC.EnableMovement();
        }

        sound.Play(0);

        Destroyaudio();

    }
    public void Destroyaudio()
    {
        
        AudioSource.Destroy(sound, .15f);
    }
}
