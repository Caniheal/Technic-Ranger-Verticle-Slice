using UnityEngine;
using System.Collections;

public class GrapplingHook : MonoBehaviour
{
    public Transform cam;
    private RaycastHit hit;
    private Rigidbody rb;
    public bool attatched = false;
    private float momentum;
    public float speed;
    private float step;
    public RigidBodyFirstPersonController cc;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            {
                if (Physics.Raycast(cam.position, cam.forward, out hit))
                {
                    cc.mouseLook.XScensitivity = 0;
                    cc.mouseLook.YScensitivity = 0;
                    attatched = true;
                    rb.isKinematic = true;
                }
            }
            if (Input.GetButtonDown("Fire1"))
                cc.mouseLook.XScensitivity = 5;
            cc.mouseLook.YScensitivity = 5;
            attatched = true;
            rb.velocity = cam.forward * momentum;
        }
        if (attatched)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }
        if (!attatched && momentum <= 0)
        {
            momentum -= Time.deltaTime * 5;
            step = 0;
        }
    }
}