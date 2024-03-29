﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Newtonsoft.Json.Linq;
using System.IO;
using static SceneSwitching;
using static Paused;
using Newtonsoft.Json;

public enum WeaponState
{
    Default,
    Vista,
    Anchor,
    Shield,
}

public class PlayerController : MonoBehaviour
{
    //For our animations
    private float DpadX;
    private float DpadY;
    public Animator anim;
    public Text countText;
    public int count;
   


    public float MovementSpeed = 1;
    public float MouseSensitivity = 1;
    public float CameraDistance = 3;
    public float CameraSphereTestRadius = .3f;
    public Vector3 CameraOffset;
    public WeaponState CurrentWeaponState = WeaponState.Default;

    public float MinCameraDistance = .1f;
    public float Gravity = 9.8f;
    public float FallingMultiplier = 2.5f;
    public float JumpingMultiplier = 2.0f;
    public float AirControlMultiplier = .5f;

    public float WarpFirstTestRadius = .3f;
    public float WarpSecondTestRadius = 1;

    public float JumpSpeed = 8;
    public float TotalSlideTime = 2f;

    public Camera Camera;
    public WarpManager WarpManager;
    public GameObject ShieldPrefab;
    public bool IsOnShield;

    // color swaping references
    public Material Purple, Teal, Tan, Blue, White, Black;
    public SkinnedMeshRenderer Render;
    public GameObject VistaHalo, AnchorLauncher, Shield;


    // reference for reticle
    public Image reticle;

    //Sound Stuff by Fran and Nora!
    private AudioSource source;
    public AudioClip jumpClip;
    public AudioClip walkClip;
    public AudioClip landingClip;
    public AudioClip slideClip;
    public AudioClip maskSwitchClip;
    public AudioClip createPortalClip;
    public AudioClip createBoardClip;
    public AudioClip fireBoltClip;
    public AudioClip hitClip;
    public AudioClip activateClip;
    public AudioClip destroyClip;

    //Particle Effects
    public ParticleSystem defaultMaskEffect;
    public ParticleSystem vistaMaskEffect;
    public ParticleSystem anchorMaskEffect;
    public ParticleSystem shieldMaskEffect;


    //Welcome to Lylly's notes in the script. :)
    // Euler Angle (rotation) is when... x = pitch; y = yaw; z= roll
    public float Yaw;
    public float Pitch;
    public Vector3 MoveDirection;

    //Nora fiddling with acceleration: the variables
    /*{
    public Vector3 acceleration_2;
    public float speed;
    public Vector3 moveVector;
    public Vector3 velocity_2;
    }*/

    //fiddling with sliding
    private double tempSpeedX;
    private double tempSpeedY;

    private CharacterController characterController;
    private CapsuleCollider capsuleCollider;
    private bool movementEnabled = true;
    private GameObject SpawnedShield;

    private Vector3 forward;
    private bool IsSliding = false;
    private float slideTimer = 0;
    private Vector3 groundNormal;

    private bool warpGhostMode = false;
    private float warpToggleCooldown = 0;


    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        forward = transform.forward;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;


        // make it so the reticle and weapons isnt shown by default
        reticle.enabled = false;
        VistaHalo.GetComponent<MeshRenderer>().enabled = false;
        AnchorLauncher.GetComponent<MeshRenderer>().enabled = false;
        Shield.GetComponent<MeshRenderer>().enabled = false;

        SaveData o1 = LoadFile();
        count = (int)o1.coinCount;
        SetCountText();
    }

    private static SaveData LoadFile()
    {
        //Paused paused = new Paused();
        
        // if static: CLASSNAME.WHATYOUWANT
        // if not: CLASS INSTANCE.WHATYOUWANT

        SaveData savedFile = JsonConvert.DeserializeObject<List<SaveData>>(File.ReadAllText(Paused.fileName))[0];
        return savedFile;
    }

    void SetCountText()
    {
        countText.text = "Bits Collected: " + count.ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();

            Paused.Save(count);
        }
    }

    /*public void SpawnAtCheckpoint()
    {
        DisableMovement();
        this.transform.position = GameManager.Instance.lastCheckPoint.position;
        EnableMovement();

    }*/

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        groundNormal = hit.normal;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void DisableMovement()
    {
        movementEnabled = false;
        characterController.enabled = false;
        capsuleCollider.enabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
        characterController.enabled = true;
        capsuleCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale <= 0f)
        {
            return;
        }

        UpdateWeapon();
        UpdateSpawnShield();
        UpdateMovement();
        UpdateCamera();
        UpdateWarper();
        UpdateAnchor();
        UpdateDefault();
        UpdateScene();
    }


    void UpdateMovement()
    {
        if (!movementEnabled)
        {
            return;
        }

        bool IsCrouching = Input.GetKey(KeyCode.LeftControl) || Input.GetButton("B Button");

        //Our "forward" is the same as the camera
        //Zero out the y so you don't fly up or fall down 
        Vector3 cameraF = Camera.transform.forward;
        cameraF.y = 0f;

        if (cameraF.magnitude > .3f)
        {
            forward = Camera.transform.forward.normalized;
            forward.y = 0f;
        }
        else
        {
            forward = forward.normalized * .8f;
        }

        Vector3 right = Camera.transform.right;
        right.y = 0f;


        anim.SetBool("isSliding", IsSliding);
        anim.SetBool("isCrouching", IsCrouching);

        if (!IsSliding)
        {
            //Default when you're not moving
            MoveDirection = Vector3.zero;

            MoveDirection += forward * Input.GetAxis("Vertical");
            MoveDirection += right * Input.GetAxis("Horizontal");

            MoveDirection = MoveDirection * MovementSpeed;

            slideTimer += Time.deltaTime;
        }
        else
        {
            MoveDirection += -MoveDirection * .035f;
            MoveDirection.y = 0;

            if (!IsCrouching)
            {
                slideTimer = 0f;
                IsSliding = false;
            }
        }

        //How far you can move when in air
        //! = not
        if (!characterController.isGrounded)
        {
            MoveDirection = MoveDirection * AirControlMultiplier;
        }

        // jump
        if (!IsSliding && characterController.isGrounded && Input.GetButton("Jump"))
        {
            MoveDirection += Vector3.up * JumpSpeed;

            //jumpsound
            source.PlayOneShot(jumpClip);

            anim.SetTrigger("jump");
        }

        //current character velocity
        if (characterController.velocity.y < 0)
        {
            //Down
            MoveDirection += Vector3.up * Gravity * FallingMultiplier * Time.deltaTime;
        }
        else
        {
            //Up
            MoveDirection += Vector3.up * Gravity * JumpingMultiplier * Time.deltaTime;
        }



        //For TUTORIAL RAMP, Sliding down with quicker speed (WIP)
        /*if (characterController.isGrounded)
        {
            MoveDirection.x += (1f - groundNormal.y) * groundNormal.x * 6 * .03f;
            MoveDirection.z += (1f - groundNormal.y) * groundNormal.z * 6 * .03f;
        }*/



        //maintain up/down velocity; continue to move the way ya moving
        MoveDirection.y += characterController.velocity.y;

        //set y (pitch) to ZERO because we don't cause about up and down direction
        Vector3 XZMoveDirection = MoveDirection.normalized;
        XZMoveDirection.y = 0f;

        //check to see if we're moving
        if (XZMoveDirection.magnitude > .1f)
        {
            anim.SetBool("isRunning", true);
            gameObject.transform.rotation = Quaternion.LookRotation(XZMoveDirection.normalized, Vector3.up);

            if (IsCrouching && !IsSliding && slideTimer > TotalSlideTime && XZMoveDirection.magnitude > .3f && CurrentWeaponState == WeaponState.Default)
            {
                IsSliding = true;

                //tempSpeedX = MoveDirection.x * 1.5;
                //Adjust Power Slide Here, 
                MoveDirection.x *= 3f;
                MoveDirection.z *= 3f;
                //source.PlayOneShot(slideClip);
            }
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
  
        //Telling CharacterConroller to move in this direction (Also moveDirec*moveSpeed = velocity)
        characterController.Move(MoveDirection * Time.deltaTime);
        //nora fiddling with acceleration: the code
        /*{
            
        }*/
    }

    void UpdateCamera()
    {
        //Keep yaw between -180 and 180 (360 camera rotation)
        //adding to yaw
        /* commenting this out because it spams the debug.log
        Debug.Log(Input.GetAxis("Mouse X"));
        Debug.Log(Input.GetAxis("Mouse Y"));
        Debug.Log("----");
        */

        Yaw += MouseSensitivity * Input.GetAxis("Mouse X");

        //Keep pitch between -90 and 90 (180 camera rotation) to not flip backwards
        //pitch gets inverted so subtract
        Pitch -= MouseSensitivity * Input.GetAxis("Mouse Y");
        Pitch = Mathf.Clamp(Pitch, -89, 89);

        //make rotation from euler angles
        Quaternion rotation = Quaternion.Euler(Pitch, Yaw, 0);



        Vector3 focusPosition = CameraOffset + transform.position;

        //                          capsule position + rotation of mouse * back(moving camera back so it's not inside you)
        Vector3 desiredCameraPosition = focusPosition + rotation * Vector3.back * CameraDistance;

        Vector3 cameraPosition = Camera.transform.position;
        Vector3 capsuleToCameraDirection = (desiredCameraPosition - focusPosition).normalized;

        //calculating distance between 2 points; float = the value
        float cameraMoveDistance = Vector3.Distance(focusPosition, desiredCameraPosition);

        //output of the raycast/spherecast 
        RaycastHit hit;


        //Moves the camera(thats in desired location) to the hit point 
        if (Physics.SphereCast(focusPosition, CameraSphereTestRadius, capsuleToCameraDirection, out hit, cameraMoveDistance))
        {
            desiredCameraPosition = hit.point;

            if (hit.distance < MinCameraDistance)
            {
                //desiredCameraPosition = Camera.transform.position;
            }
        }


        Camera.transform.position = desiredCameraPosition;

        //cameraToCapsule = way the camera is looking to the character
        //                        capsule position - (ABOVE; camera position) ....normalized(makes everything between 0-1 <--  so we know the DIRECTION #PHYSICS)
        Vector3 cameraToCapsuleDirection = (focusPosition - Camera.transform.position).normalized;

        //Direction lets us compute a rotation(camera) from that direction; rotating camera to face capsule
        //idk why it's .up but it told me to write .up so i will write .up
        Camera.transform.rotation = Quaternion.LookRotation(cameraToCapsuleDirection, Vector3.up);
    }

    
    /// <summary>
    /// NOTE FOR TIM: COPY THE WARPER LAYOUT, MAKE A FUNCTION LIKE UPDATE WARPER, CHECK IF THE CURRENT WEAPONSTATE IS "ANCHOR"
    ///THEN SET TO BUTTON CLICK THAT YOU CARE ABOUT (mouse0) (Its the same as the warper, copy and paste the first 2 if statements)
    /// </summary>
    void UpdateWarper()
    {
        Material[] materials = Render.materials;

        if (CurrentWeaponState == WeaponState.Vista)
        {
            VistaHalo.GetComponent<MeshRenderer>().enabled = true;

            if (WarpManager.IsWarperActive())
            {
                //ANCHOR
                materials[0] = Black;
            }
            else
            {
                //ANCHOR
                materials[0] = Purple;
            }
        }

        Render.materials = materials;

        bool rightBumper = Input.GetButton("Right Bumper");

        if (CurrentWeaponState == WeaponState.Vista)
        {
            warpToggleCooldown -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetButton("Right Bumper"))
            {
                if (warpToggleCooldown <= 0)
                {
                    //Toggle for ghost mode 
                    //If we are in ghost we turn it off; if not, we turn it on
                    if (warpGhostMode)
                    {

                        //on --> off
                        if (WarpManager)
                        {
                            //VistaHalo.GetComponent<MeshRenderer>().enabled = false;
                            Debug.Log("Place");
                            WarpManager.CompletWarpPlacement();
                            source.PlayOneShot(createPortalClip);
                        }

                        warpGhostMode = false;
                    }

                    //off --> on
                    else
                    {
                        Debug.Log("Start");

                        warpGhostMode = true;
                        //VistaHalo.GetComponent<MeshRenderer>().enabled = true;
                    }

                    warpToggleCooldown = .5f;
                }
                
            }

            if (warpGhostMode)
            {
                Vector3 CameraDirection = Camera.transform.rotation * Vector3.forward;
                CameraDirection.y = 0;
 
                if (WarpManager)
                {
                    WarpManager.PlaceWarper(transform.position, transform.position + CameraDirection * 3, CameraDirection);
                }
            }
        }
        else
        {
            warpToggleCooldown = 0;
            warpGhostMode = false;
            VistaHalo.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    // Anchor fuctionallity
    void UpdateAnchor()
    {
        Material[] materials = Render.materials;

        if (CurrentWeaponState == WeaponState.Anchor)
        {
            

            //ANCHOR
            materials[0] = Teal;
            AnchorLauncher.GetComponent<MeshRenderer>().enabled = true;
        }

        Render.materials = materials;

        bool rightBumper = Input.GetButton("Right Bumper");

        if (CurrentWeaponState == WeaponState.Anchor)
        {
            reticle.enabled = true;

            /*if (Input.GetKey(KeyCode.Mouse1) || Input.GetButton("Left Trigger"))
            {
                reticle.enabled = true;
            }
            else
            {
                reticle.enabled = false;
            }*/

            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("Right Bumper"))
            {
                RaycastHit hit;
                source.PlayOneShot(fireBoltClip);
                // Does the ray intersect any objects excluding the player layer
                if (Physics.Raycast(Camera.transform.position, Camera.transform.forward, out hit, Mathf.Infinity))
                {
                    if (hit.collider != null)
                    {
                        var anchorAnimation = hit.collider.gameObject.GetComponent<AnchorAnimation>();
                        source.PlayOneShot(hitClip);
                        if (anchorAnimation != null)
                        {
                            anchorAnimation.OnRayHit();
                            source.PlayOneShot(activateClip);
                            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                            Debug.Log("Did Hit");
                        }
                    }
                }
                else
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
                    Debug.Log("Did not Hit");
                }
            }
        }
        else
        {
            AnchorLauncher.GetComponent<MeshRenderer>().enabled = false;
            reticle.enabled = false;
        }
    }

    void UpdateSpawnShield()
    {
        Material[] materials = Render.materials;

        if (CurrentWeaponState == WeaponState.Shield)
        {
            

            //ANCHOR
            materials[0] = White;
        }

        Render.materials = materials;

        if (CurrentWeaponState == WeaponState.Shield)
        {
            //ALWAYS set these to false unlessss..... (below)
            anim.SetBool("isShieldLefting", false);
            anim.SetBool("isShieldRighting", false);
            anim.SetBool("isRunning", false);

            //The ranger is on the shield :) Then take in values
            if (IsOnShield)
            {
                float HorizontalValue = Input.GetAxis("Horizontal");
                Debug.Log(HorizontalValue);
                if (HorizontalValue > 0f)
                {
                    anim.SetBool("isShieldLefting", true);
                }
                else if (HorizontalValue < 0f)
                {
                    anim.SetBool("isShieldRighting", true);
                }
            }

            if ((Input.GetKey(KeyCode.Mouse0) || Input.GetButton("Right Bumper")) && SpawnedShield == null)
            {
                Vector3 spawnLocation = transform.position + (transform.rotation * Vector3.forward * 2f);
                spawnLocation.y += -.5f;
                SpawnedShield = Instantiate(ShieldPrefab, spawnLocation, Quaternion.identity);
                source.PlayOneShot(createBoardClip);
            }
            if (SpawnedShield == null)
            {
                Shield.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                Shield.GetComponent<MeshRenderer>().enabled = false;

            }
        }
        else
        {
            Shield.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    void UpdateDefault()
    {
        Material[] materials = Render.materials;

        if (CurrentWeaponState == WeaponState.Default)
        {
            

            //ANCHOR
            materials[0] = Blue;
        }

        Render.materials = materials;
    }



    //Adds to list (LINE 537ish as of 3/19/2020)
    public void UnlockWeapon(WeaponState WeaponType)
    {
        PersistentData.Instance.UnlockedWeapons.Add(WeaponType);
    }


    //Gets the weapons from persistantData script
    public List<WeaponState> GetUnlockedWeapons()
    {
        if (PersistentData.Instance)
        {
            return PersistentData.Instance.UnlockedWeapons;
        }

        return new List<WeaponState>();
    }


    //OUR DEV CHEATS :)
    void UpdateScene()
    {
        if (Input.GetKeyDown("8"))
        {
            SceneManager.LoadScene("Jungle");
        }


        if (Input.GetKeyDown("9"))
        {
            SceneManager.LoadScene("Temple");
        }


        if (Input.GetKeyDown("0"))
        {
            SceneManager.LoadScene("Mines");
        }

    
    }

    void UpdateWeapon()
    {

        float DpadX = Input.GetAxis("Dpad X");
        float DpadY = Input.GetAxis("Dpad Y");
   
        if (WarpManager.IsWarperActive())
        {
            //return = exiting out of this function
            return;
        }



        WeaponState NewWeaponState = CurrentWeaponState;

        if (Input.GetKeyDown("1") || (Input.GetAxis("Dpad Y") < 0))
        {
            NewWeaponState = WeaponState.Default;
        }
        if (Input.GetKeyDown("2") || (Input.GetAxis("Dpad Y") > 0))
        {
            NewWeaponState = WeaponState.Vista; 
        }
        if (Input.GetKeyDown("3") || (Input.GetAxis("Dpad X") > 0))
        {
            NewWeaponState = WeaponState.Anchor;    
        }

        if (Input.GetKeyDown("4") || (Input.GetAxis("Dpad X") < 0))
        {
            NewWeaponState = WeaponState.Shield;
        }



        //                                          Unlocked Weapons list CHECK (If you have the pickup)
        //                                           YES = switch to weapon; NO = don't switch
        if (NewWeaponState != CurrentWeaponState && GetUnlockedWeapons().Contains(NewWeaponState))
        {

            source.PlayOneShot(maskSwitchClip);

            if (NewWeaponState == WeaponState.Default)
            {
                Instantiate(defaultMaskEffect, transform.position, Quaternion.identity);
            }
            else if (NewWeaponState == WeaponState.Anchor)
            {
                Instantiate(anchorMaskEffect, transform.position, Quaternion.identity);
            }
            else if (NewWeaponState == WeaponState.Vista)
            {
                Instantiate(vistaMaskEffect, transform.position, Quaternion.identity);
            }
            else if (NewWeaponState == WeaponState.Shield)
            {
                Instantiate(shieldMaskEffect, transform.position, Quaternion.identity);
            }





            if (CurrentWeaponState == WeaponState.Vista)
            {
                WarpManager.DisableWarper();
                source.PlayOneShot(destroyClip);
            }

            //CALL EXIT SHIELD when switching masks :)
  
            if (CurrentWeaponState == WeaponState.Shield)
            {
                if (SpawnedShield)
                {
                    SkateboardController Skateboard = SpawnedShield.GetComponent<SkateboardController>();

                    Skateboard.ExitShield();

                    SpawnedShield = null;
                }

                source.PlayOneShot(destroyClip);
            }

           /* Debug.Log("changed update colors");

            ColorSwapper.UpdateColors(CurrentWeaponState); */

            CurrentWeaponState = NewWeaponState; 
        }
    }
}
