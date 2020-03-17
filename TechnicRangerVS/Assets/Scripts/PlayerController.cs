using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    public float TotalSlideTime = 1.5f;

    public Camera Camera;
    public WarpManager WarpManager;
    public GameObject ShieldPrefab;
    // color swaping references
    public Material Purple, Teal, Tan, Blue, White;
    public SkinnedMeshRenderer Render;


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
    private bool movementEnabled = true;
    private GameObject SpawnedShield;

    private Vector3 forward;
    private bool IsSliding = false;
    private float slideTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        forward = transform.forward;

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;

        // make it so the reticle isnt shown by default
        reticle.enabled = false;
    }

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void DisableMovement()
    {
        movementEnabled = false;
        characterController.enabled = false;
    }

    public void EnableMovement()
    {
        movementEnabled = true;
        characterController.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateWeapon();
        UpdateSpawnShield();
        UpdateMovement();
        UpdateCamera();
        UpdateWarper();
        UpdateAnchor();
        UpdateDefault();
    }


    void UpdateMovement()
    {
        if (!movementEnabled)
        {
            return;
        }

        bool IsCrouching = Input.GetKey(KeyCode.LeftControl);

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
        }
        else
        {

            slideTimer += Time.deltaTime;

            if (slideTimer >= TotalSlideTime)
            {

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

            if (IsCrouching && !IsSliding && XZMoveDirection.magnitude > .3f)
            {
                IsSliding = true;
                //tempSpeedX = MoveDirection.x * 1.5;
                MoveDirection.x *= 1.5f;
                MoveDirection.z *= 1.5f;
                MoveDirection.y = 0f;
                source.PlayOneShot(slideClip);
                slideTimer = 0f;
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
                desiredCameraPosition = Camera.transform.position;
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

    void UpdateWarper()
    {
        Material[] materials = Render.materials;

        if (CurrentWeaponState == WeaponState.Vista)
        {
           

            //ANCHOR
            materials[0] = Purple;
        }

        Render.materials = materials;

        bool rightBumper = Input.GetButton("Right Bumper");

        if (CurrentWeaponState == WeaponState.Vista)
        {
            if (Input.GetKey(KeyCode.Mouse0) || Input.GetButton("Right Bumper"))
            {
                Vector3 CameraDirection = Camera.transform.rotation * Vector3.forward;
                RaycastHit hit;

                Debug.DrawLine(Camera.transform.position, Camera.transform.position + CameraDirection * 100, Color.red, 30);

                if (Physics.SphereCast(Camera.transform.position, WarpFirstTestRadius, CameraDirection, out hit, 10f))
                {
                    Debug.Log(hit.collider);

                    CameraDirection.y = 0;

                    if (WarpManager)
                    {
                        WarpManager.PlaceWarper(hit.point, CameraDirection);
                        source.PlayOneShot(createPortalClip);
                    }
                }
                else
                {
                    // no warping cast sound
                }
            }
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
        }

        Render.materials = materials;

        bool rightBumper = Input.GetButton("Right Bumper");

        if (CurrentWeaponState == WeaponState.Anchor)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                reticle.enabled = true;
            }
            else
            {
                reticle.enabled = false;
            }

            if (Input.GetKey(KeyCode.Mouse0) || Input.GetButton("Right Bumper"))
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
            if (Input.GetKey(KeyCode.Mouse0) && SpawnedShield == null)
            {
                Vector3 spawnLocation = transform.position + (transform.rotation * Vector3.forward * 2f);
                spawnLocation.y += -.5f;
                SpawnedShield = Instantiate(ShieldPrefab, spawnLocation, Quaternion.identity);
                source.PlayOneShot(createBoardClip);
            }
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
            source.PlayOneShot(maskSwitchClip);
            
        }
        if (Input.GetKeyDown("2") || (Input.GetAxis("Dpad Y") > 0))
        {
            NewWeaponState = WeaponState.Vista;
            source.PlayOneShot(maskSwitchClip);
        }
        if (Input.GetKeyDown("3") || (Input.GetAxis("Dpad X") > 0))
        {
            NewWeaponState = WeaponState.Anchor;
            source.PlayOneShot(maskSwitchClip);
        }

        if (Input.GetKeyDown("4") || (Input.GetAxis("Dpad X") < 0))
        {
            NewWeaponState = WeaponState.Shield;
            source.PlayOneShot(maskSwitchClip);
        }

        if (NewWeaponState != CurrentWeaponState)
        {
            if (CurrentWeaponState == WeaponState.Vista)
            {
                WarpManager.DisableWarper();
                source.PlayOneShot(destroyClip);
            }

            if (CurrentWeaponState == WeaponState.Shield)
            {
                Destroy(SpawnedShield);
                SpawnedShield = null;
                source.PlayOneShot(destroyClip);
            }

           /* Debug.Log("changed update colors");

            ColorSwapper.UpdateColors(CurrentWeaponState); */

            CurrentWeaponState = NewWeaponState; 
        }
    }
}
