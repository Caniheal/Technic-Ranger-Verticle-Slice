using UnityEngine;
using System.Collections;



public class EnterSkate : MonoBehaviour

{
    private bool inVehicle = false;
    SkateboardController vehicleScript;
    GameObject player;


    void Start()
    {
        vehicleScript = GetComponent<SkateboardController>();
        player = GameObject.FindWithTag("Player");
    }


    // Update is called once per frame

    void OnTriggerStay(Collider other)

    {

        if (other.gameObject.tag == "Player" && inVehicle == false)
        {

            if (Input.GetKey(KeyCode.E))

            {
                player.transform.parent = gameObject.transform;
                vehicleScript.enabled = true;
                player.SetActive(false);
                inVehicle = true;
            }
        }
    }

    void OnTriggerExit(Collider other)

    {
        if (other.gameObject.tag == "Player")
        {

        }
    }

    void Update()

    {
        if (inVehicle == true && Input.GetKey(KeyCode.F))
        {
            vehicleScript.enabled = false;
            player.SetActive(true);
            player.transform.parent = null;
            inVehicle = false;
        }
    }
}
