using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMove : MonoBehaviour
{
    public float DistanceToMove = 6;
    public float Speed = 1;
    public GameObject Player;

    //Vector 3 = DirectionToMoveXYZ <-- SAME SHIT
    public Vector3 DirectionToMove = new Vector3(1, 0, 0);

    private float currentDistance;

    // Use this for initialization
    void Start()
    {
        currentDistance = 0;
    }
    // child player to platfrom
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = transform;
        }
    }
    // unchild player to platfrom
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        //Vector 3 stores transform location info
        Vector3 PreviousLocation = transform.position;

        // (delta time) * speed * Vector3(1, 0, 0) <-- Can edit later 

        //  DirectionToMove.X = DirectionToMove.X * .333 * 1;
        //  DirectionToMove.Y = DirectionToMove.Y * .333 * 1;
        //  DirectionToMove.Z = DirectionToMove.Z * .333 * 1;


        // Direction moved at __ speed during __ period of time.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("working");
            Vector3 PositionWeWantToMoveTo = Time.deltaTime * Speed * DirectionToMove;

            transform.position = transform.position + (PositionWeWantToMoveTo);

            /*if (transform.position != PreviousLocation)
            {
                currentDistance = currentDistance + Vector3.Distance(PreviousLocation, transform.position);
            }*/

        }
    }
   

    
}

