using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Transform checkpoint;
    GameObject player;
    private AudioSource source;
    public AudioClip save;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.transform.position = checkpoint.position;
            source.PlayOneShot(save);
        }
    }
}
