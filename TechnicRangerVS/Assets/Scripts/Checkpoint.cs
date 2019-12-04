using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Transform checkpoint;
    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.transform.position = checkpoint.position;
        }
    }
}
