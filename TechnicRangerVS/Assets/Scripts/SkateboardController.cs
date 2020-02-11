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
        }
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
            if (Input.GetKey(KeyCode.Space))
            {
                player.transform.parent = null;
                player.EnableMovement();
                player.transform.rotation = Quaternion.identity;
                player = null;
                enabletriggerenter = false;
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
