using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject PrefabStartWarper;
    public GameObject PrefabEndWarper;
    public GameObject PrefabGhost;

    public GameObject StartWarper;
    public GameObject EndWarper;
    public GameObject GhostWarper;

    public float WarpTime = 1;
    public float DistanceToWarp = 1;

    //How big sphere cast balls are ;D
    public float WarperTestRadius = 1;
    //How long sphere cast balls are ;D
    public float WarpTestDistance = 3;

    public Vector3 WarperOffset = new Vector3(0f, 3f, 0f);

    //This is the warp; not in use
    public bool isActive = false;
    public bool wasWarperSpawned = false;

    private PlayerController player;

    private float currentTime = 0;

    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            currentTime += Time.deltaTime;

            //Need this ratio to go from 0-1 (total distance)
            float lerpRatio = currentTime / WarpTime;

            //Lerp ratio; EXAMPLE; percentage between start/end values
            // float start = 10;
            //float end = 20;
            // lerpRatio(10, 20, 0) = 10;
            //lerpRatio(10, 20, .5) = 15;
            //lerpRatio(10, 20, 1) = 20;


            Vector3 currentPostion = Vector3.Lerp(StartWarper.transform.position, EndWarper.transform.position, lerpRatio);

            player.gameObject.transform.position = currentPostion;

            //END WARPER STUFFS
            if (currentTime >= WarpTime)
            {
                //current time larger than 1
                //timer to despawn starts when we reach the end warper
                isActive = false;
                wasWarperSpawned = true;
                //Set to 0 so we can count up to te despawn time
                currentTime = 0f;
                Vector3 EndPostion = EndWarper.transform.position;

                player.gameObject.transform.position = EndPostion;

                player.EnableMovement();
            }
        }
        else if (wasWarperSpawned)
        {
            //THE DESPAWN TIMER INCREMENTING 
            currentTime += Time.deltaTime;

            if (currentTime >= WarpTime)
            {
                currentTime = 0f;
                wasWarperSpawned = false;
                DisableWarper();
            }
        }

        if (StartWarper && StartWarper.activeSelf)
        {
            //distance from the player to the startwarper 
            float DistanceToStartWarper = Vector3.Distance(StartWarper.transform.position, player.transform.position);

            // Debug.Log(DistanceToStartWarper);
            if (DistanceToStartWarper < DistanceToWarp)
            {
                Warp();
            }
        }
    }

    public bool IsWarperActive()
    {
        return isActive;
    }

    public void DisableWarper()
    {
        if (StartWarper)
        {
            StartWarper.SetActive(false);
            EndWarper.SetActive(false);
            GhostWarper.SetActive(false);
        }
    }

    //public lets other scripts call the function
    //Where the warper is going to be placed in the world
    public void PlaceWarper(Vector3 startPosition, Vector3 placePosition, Vector3 placedDirection)
    {
        if (isActive)
        {
            return;
        }

        RaycastHit hit;
        //multiply so we can move that distance (in that direction)
        Vector3 endWarperLocation = startPosition + placedDirection * WarpTestDistance;

        //Where the warper is places, how big the sphere cast is, what direction we're testing, reported hits (walls), how long the sphere cast is
        if (Physics.SphereCast(placePosition, WarperTestRadius, placedDirection, out hit, WarpTestDistance))
        {
            //if we hit a wall, put end warper at that wall
            endWarperLocation = hit.point;
        }

        if (StartWarper == null)
        {
            StartWarper = Instantiate(PrefabStartWarper);
            EndWarper = Instantiate(PrefabEndWarper);
            GhostWarper = Instantiate(PrefabGhost);
        }

        StartWarper.SetActive(false);
        EndWarper.SetActive(false);
        GhostWarper.SetActive(true);

        Quaternion rotation = Quaternion.LookRotation(placedDirection, Vector3.up);

        StartWarper.transform.rotation = rotation;
        EndWarper.transform.rotation = rotation;
        GhostWarper.transform.rotation = EndWarper.transform.rotation;

        StartWarper.transform.position = placePosition + WarperOffset;
        EndWarper.transform.position = endWarperLocation + WarperOffset;
        GhostWarper.transform.position = EndWarper.transform.position;

    }

    public void CompletWarpPlacement()
    {
        StartWarper.SetActive(true);
        EndWarper.SetActive(true);
        GhostWarper.SetActive(false);
    }

    //Warp activated; in use STARTING
    void Warp()
    {
        if (!isActive)
        {
            wasWarperSpawned = false;
            isActive = true;
            player.DisableMovement();

            //current time in realm always starts at 0
            currentTime = 0;
        }
    }

}
