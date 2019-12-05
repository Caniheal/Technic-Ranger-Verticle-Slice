using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //For our animations
    private Animator anim;

    public float MovementSpeed = 1;
    public float MouseSensitivity = 1;
    public float CameraDistance = 3;
    public float CameraSphereTestRadius = .3f;
    public Vector3 CameraOffset;

    public float MinCameraDistance = .1f;
    public float Gravity = 9.8f;
    public float FallingMultiplier = 2.5f;
    public float JumpingMultiplier = 2.0f;
    public float AirControlMultiplier = .5f;

    public float WarpFirstTestRadius = .3f;
    public float WarpSecondTestRadius = 1;

    public float JumpSpeed = 8;

    public Camera Camera;
    public WarpManager WarpManager;

    //Welcome to Lylly's notes in the script. :)
    // Euler Angle (rotation) is when... x = pitch; y = yaw; z= roll
    public float Yaw;
    public float Pitch;
    public Vector3 MoveDirection;

    private CharacterController characterController;
    private bool movementEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();

        Cursor.visible = false;

        Cursor.lockState = CursorLockMode.Locked;
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
        UpdateMovement();
        UpdateCamera();
        UpdateWarper();
    }


    void UpdateMovement()
    {
        if (!movementEnabled)
        {
            return;
        }

        //Our "forward" is the same as the camera
        //Zero out the y so you don't fly up or fall down 
        Vector3 forward = Camera.transform.forward;
        forward.y = 0;


        Vector3 right = Camera.transform.right;
        right.y = 0;

        //Default when you're not moving
        MoveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            MoveDirection += forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //back
            MoveDirection += -forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //left
            MoveDirection += -right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            MoveDirection += right;
        }

        //Animations A LITTLE JANK RN BUT IT WORKS
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetTrigger("jump");
        }


            MoveDirection = MoveDirection * MovementSpeed;

        //How far you can move when in air
        //! = not
        if (!characterController.isGrounded)
        {
            MoveDirection = MoveDirection * AirControlMultiplier;
        }

        // jump
        if (characterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            MoveDirection += Vector3.up * JumpSpeed;
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

        //Telling CharacterConroller to move in this direction (Also moveDirec*moveSpeed = velocity)
        characterController.Move(MoveDirection * Time.deltaTime);
    }



    void UpdateCamera()
    {
        //Keep yaw between -180 and 180 (360 camera rotation)
        //adding to yaw
        Yaw += MouseSensitivity * Input.GetAxis("Mouse X");

        //Keep pitch between -90 and 90 (180 camera rotation) to not flip backwards
        //pitch gets inverted so subtract
        Pitch -= MouseSensitivity * Input.GetAxis("Mouse Y");
        Pitch = Mathf.Clamp(Pitch, -90, 90);

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
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 CameraDirection = Camera.transform.rotation * Vector3.forward;
            RaycastHit hit;

            Debug.DrawLine(Camera.transform.position, Camera.transform.position + CameraDirection * 100, Color.red, 30);

            if (Physics.SphereCast(Camera.transform.position, WarpFirstTestRadius, CameraDirection, out hit, 10f))
            {
                CameraDirection.y = 0;
          
                if (WarpManager)
                {
                    WarpManager.PlaceWarper(hit.point, CameraDirection);
                }
            }
            else 
            {
                // no warping cast sound
            }
        }
    }
}
