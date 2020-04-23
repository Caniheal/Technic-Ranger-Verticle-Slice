using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    //This script is a component you put on things you wanna hook

    public float HookSpeed = 10f;
    public float HookDelay = 3f;

    Vector3 StartingPosition;
    GameObject HookOrigin;

    bool GoBackToStart = false;

    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = transform.position;
    }

    public void StartHook(GameObject NewHookOrigin)
    {
        //This is the position the object is going to move to (getting reeled into)
        HookOrigin = NewHookOrigin;
        GoBackToStart = false;

        Debug.Log("Starting Hook");
    }

    // Update is called once per frame
    void Update()
    {
        if (HookOrigin != null)
        {
            Vector3 DirectionToMove = (HookOrigin.transform.position - transform.position).normalized;
            float DistanceToHookPoint = Vector3.Distance(HookOrigin.transform.position, transform.position);
            float DistanceToMove = Mathf.Min(HookSpeed * Time.deltaTime, DistanceToHookPoint);

            Debug.Log("Hooking.." + DistanceToHookPoint + "-" + DistanceToMove);

            if (DistanceToHookPoint > .2f)
            {
                Vector3 NewLocation = transform.position + DirectionToMove * DistanceToHookPoint;
                transform.position = NewLocation;
            }
            else
            {
                Debug.Log("Hook Detaching");

                HookOrigin = null;
                GoBackToStart = true;
            }
        }

        if (GoBackToStart)
        {
            Vector3 DirectionToMove = (StartingPosition - transform.position).normalized;
            float DistanceToHookPoint = Vector3.Distance(HookOrigin.transform.position, transform.position);
            float DistanceToMove = Mathf.Min(HookSpeed * Time.deltaTime, DistanceToHookPoint);

            if (DistanceToHookPoint > .01f)
            {
                Vector3 NewLocation = transform.position + DirectionToMove * DistanceToHookPoint;
                transform.position = NewLocation;
            }
            else
            {
                Debug.Log("Hook Went back to start");

                GoBackToStart = false;
            }
        }
    }
}
