using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrophyPickup : MonoBehaviour
{
    [SerializeField] public Image customImage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            customImage.enabled = true;
        }
    }
}
