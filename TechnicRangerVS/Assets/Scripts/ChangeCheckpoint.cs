using UnityEngine;

public class ChangeCheckpoint : MonoBehaviour
{
    public GameObject checkpoint;

    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
            Destroy(checkpoint);
        Destroy(gameObject);
    }
}
