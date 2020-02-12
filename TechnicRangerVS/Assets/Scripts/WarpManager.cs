using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject StartWarper;
    public GameObject EndWarper;

    public Transform warpRealmStartPosition;
    public Transform warpRealmEndPosition;
    public Material RealmSkybox;
    public Material NormalSkybox;

    //Camera roation; ROTATION = QUATERNION
    public Quaternion warpRealmCameraOrientation;
    public float WarpTime = 1;
    public float DistanceToWarp = 1;

    //How big sphere cast balls are ;D
    public float WarperTestRadius = 1;
    //How long sphere cast balls are ;D
    public float WarpTestDistance = 3;

    //This is the warp; not in use
    private bool isActive = false;
    private PlayerController player;

    private float currentTime = 0;

    //sounds by nora and fran sorta
    private AudioSource source;
    public AudioClip destroyClip;
    public AudioClip portalEnterClip;
    public AudioClip portalExitClip;

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


            Vector3 currentPostion = Vector3.Lerp(warpRealmStartPosition.position, warpRealmEndPosition.position, lerpRatio);

            player.gameObject.transform.position = currentPostion;

            if (currentTime >= WarpTime)
            {
                //current time larger than 1
                isActive = false;
                Vector3 EndPostion = EndWarper.transform.position;
                EndPostion.y = 1.1f;

                player.gameObject.transform.position = EndPostion;
            
                player.EnableMovement();
                RenderSettings.skybox = NormalSkybox;
                source.PlayOneShot(destroyClip);
            }
        }

        if (StartWarper.activeSelf)
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
        StartWarper.SetActive(false);
        EndWarper.SetActive(false);
    }

    //public lets other scripts call the function
    //Where the warper is going to be placed in the world
    public void PlaceWarper(Vector3 placePosition, Vector3 placedDirection)
    {
        RaycastHit hit;
                                                                   //multiply so we can move that distance (in that direction)
        Vector3 endWarperLocation = placePosition + placedDirection * WarpTestDistance;

        //Where the warper is places, how big the sphere cast is, what direction we're testing, reported hits (walls), how long the sphere cast is
        if (Physics.SphereCast(placePosition, WarperTestRadius, placedDirection, out hit, WarpTestDistance))
        {
            //if we hit a wall, put end warper at that wall
            endWarperLocation = hit.point;
        }

        StartWarper.SetActive(true);
        EndWarper.SetActive(true);

        Quaternion rotation = Quaternion.LookRotation(placedDirection, Vector3.up);

        StartWarper.transform.rotation = rotation;
        EndWarper.transform.rotation = rotation;

        StartWarper.transform.position = placePosition;
        EndWarper.transform.position = endWarperLocation;
    }

    //Warp activated; in use
    void Warp()
    {
        isActive = true;
        player.DisableMovement();
        RenderSettings.skybox = RealmSkybox;

        //Teleporting player to the REALM start position
        player.gameObject.transform.position = warpRealmStartPosition.position;

        //setting camera orientation to the warp realm orientation
        player.Camera.gameObject.transform.rotation = warpRealmCameraOrientation;

        //current time in realm always starts at 0
        currentTime = 0;
    }

}
