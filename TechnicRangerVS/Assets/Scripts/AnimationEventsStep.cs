using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsStep : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] steps;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Step ()
    {
        AudioClip step = GetRandomClip();
        audioSource.PlayOneShot(step);
    }
    private AudioClip GetRandomClip()
    {
        return steps[UnityEngine.Random.Range(0, steps.Length)];
    }
}
