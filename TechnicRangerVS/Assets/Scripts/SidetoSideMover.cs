using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidetoSideMover : MonoBehaviour
{
    public float DistanceToMove = 6;
    public float Speed = 1;
    
    //Vector 3 = DirectionToMoveXYZ <-- SAME SHIT
    public Vector3 DirectionToMove = new Vector3(1, 0, 0);

    private float currentDistance;

	// Use this for initialization
	void Start ()
    {
        currentDistance = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Vector 3 stores transform location info
        Vector3 PreviousLocation = transform.position;

        // (delta time) * speed * Vector3(1, 0, 0) <-- Can edit later 

        //  DirectionToMove.X = DirectionToMove.X * .333 * 1;
        //  DirectionToMove.Y = DirectionToMove.Y * .333 * 1;
        //  DirectionToMove.Z = DirectionToMove.Z * .333 * 1;


        // Direction moved at __ speed during __ period of time.
        Vector3 PositionWeWantToMoveTo = Time.deltaTime * Speed * DirectionToMove;

        transform.position = transform.position + (PositionWeWantToMoveTo);

        // Distance between old&new locations ---> 6 then reverse direction
        currentDistance = currentDistance + Vector3.Distance(PreviousLocation, transform.position);

        //if current distance >= 6 then reverse direction
        if (currentDistance >= DistanceToMove)
        {
            //  Vector3(1, 0, 0) ->  Vector3(-1, 0, 0)
            DirectionToMove = DirectionToMove * -1;
            //starting distance before moving again set back to 0
            currentDistance = 0;
        }

        //(0,0,0) + (Speed * deltatime, 0, 0)
        // transform.position = transform.position + PositionWeWantToMoveTo;--
        //Local Position = relative to parent; Position = relative to world 

    }
}
