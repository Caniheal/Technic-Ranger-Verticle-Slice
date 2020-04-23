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
            player.IsOnShield = true;
            player.transform.parent = AttachPoint.gameObject.transform;
            player.transform.position = AttachPoint.gameObject.transform.position;

            //Snap your rotation to forward (parent)
            player.transform.localRotation = Quaternion.identity;
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
        
        //if (other.gameObject.CompareTag("Player"))
        //{
            //enabletriggerenter = true;
        //}
    }

    public void ExitShield()
    {
        if (player)
        {
            player.transform.parent = null;
            player.EnableMovement();
            player.transform.rotation = Quaternion.identity;
            player.IsOnShield = false;
            player = null;
            enabletriggerenter = false;
            Destroy(gameObject, 1);
        }
    }

    public void FixedUpdate()
    {
        if (player)
        {
            if (Input.GetButton("Jump"))
            {
                ExitShield();
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
        }
    }
}
