using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidetoSideMover : MonoBehaviour
{
    public float DistanceToMove = 6;
    public float Speed = 1;
    public bool StopAtEnd = true;
    public Transform OverrideTransform = null;
    public CharacterStats CharacterStats;

    //mover from rollaball
    //Vector 3 = DirectionToMoveXYZ <-- SAME SHIT
    public Vector3 DirectionToMove = new Vector3(1, 0, 0);

    private float currentDistance;
    private bool IsComplete = false;
    private Transform currentTransform;

    // Use this for initialization
    void Start ()
    {
        currentDistance = 0;

        currentTransform = transform;
        if (OverrideTransform != null)
        {
            currentTransform = OverrideTransform;
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		

        //Vector 3 stores transform location info
        Vector3 PreviousLocation = currentTransform.position;

        // (delta time) * speed * Vector3(1, 0, 0) <-- Can edit later 

        //  DirectionToMove.X = DirectionToMove.X * .333 * 1;
        //  DirectionToMove.Y = DirectionToMove.Y * .333 * 1;
        //  DirectionToMove.Z = DirectionToMove.Z * .333 * 1;


        // Direction moved at __ speed during __ period of time.
        float SpeedMultiplier = 1f;

        //This multiples everything by the speed multiplier so everything is ZERO when frozen
        if (CharacterStats)
        {
            SpeedMultiplier = CharacterStats.MoveSpeedMultiplier;
        }
        
        Vector3 PositionWeWantToMoveTo = Time.deltaTime * Speed * SpeedMultiplier * DirectionToMove;




        currentTransform.position = currentTransform.position + (PositionWeWantToMoveTo);

        // Distance between old&new locations ---> 6 then reverse direction
        currentDistance = currentDistance + Vector3.Distance(PreviousLocation, currentTransform.position);

        //if current distance >= 6 then reverse direction
        if (currentDistance >= DistanceToMove)
        {
   
            if (StopAtEnd)
            {
                //IsComplete = true;
				DirectionToMove = new Vector3 (0, 0, 0);
            }
			else
			//  Vector3(1, 0, 0) ->  Vector3(-1, 0, 0)
			{
				DirectionToMove = DirectionToMove * -1;
			}
			//starting distance before moving again set back to 0
			currentDistance = 0;
        }

		//if (IsComplete)
		//{
		//	return;
		//}


        //(0,0,0) + (Speed * deltatime, 0, 0)
        // transform.position = transform.position + PositionWeWantToMoveTo;--
        //Local Position = relative to parent; Position = relative to world 

    }

	void moveUp()
	{
		DirectionToMove = new Vector3 (0, 1, 0);
	}

	void moveDown()
	{
		DirectionToMove = new Vector3 (0, -1, 0);
	}

	void moveForward()
	{
		DirectionToMove = new Vector3 (1, 0, 0);
	}

	void moveBack()
	{
		DirectionToMove = new Vector3 (-1, 0, 0);
	}

	void moveRight()
	{
		DirectionToMove = new Vector3 (0, 0, -1);
	}

	void moveLeft()
	{
		DirectionToMove = new Vector3 (0, 0, 1);
	}

	

}
