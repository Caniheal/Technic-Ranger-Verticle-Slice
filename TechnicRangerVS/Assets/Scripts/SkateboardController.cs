using System.Collections.Generic;
using UnityEngine;

public class SkateboardController : MonoBehaviour
{
    [System.Serializable]
    public class AxleInfo
    {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor;
        public bool steering;
    }

    public GameObject AttachPoint;
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    //public GameObject Player;



    PlayerController player;
    bool enabletriggerenter = true;

    private void OnTriggerEnter(Collider other)
    {
        player = other.gameObject.GetComponent<PlayerController>();

        if (player && enabletriggerenter)
        {
            player.DisableMovement();
            player.transform.parent = AttachPoint.gameObject.transform;
            player.transform.position = AttachPoint.gameObject.transform.position;
            GetComponent<Collider>().enabled = false;
        }

        /*if (Input.GetButton("Jump"))
        {
            player.transform.parent = null;
            player.EnableMovement();
            player.transform.rotation = Quaternion.identity;
            player = null;
            enabletriggerenter = false;
        }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            enabletriggerenter = true;
        }
    }

    public void FixedUpdate()
    {
        //if (player)
        //{
            if (Input.GetButton("Jump"))
            {
                player.transform.parent = null;
                player.EnableMovement();
                player.transform.rotation = Quaternion.identity;
                player = null;
                enabletriggerenter = false;
                Destroy(gameObject, 3);
            }

            float motor = maxMotorTorque * Input.GetAxis("Vertical");
            float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

            foreach (AxleInfo axleInfo in axleInfos)
            {
                if (axleInfo.steering)
                {
                    axleInfo.leftWheel.steerAngle = steering;
                    axleInfo.rightWheel.steerAngle = steering;
                }

                if (axleInfo.motor)
                {
                    axleInfo.leftWheel.motorTorque = motor;
                    axleInfo.rightWheel.motorTorque = motor;
                }
            }
        //}
    }
}
