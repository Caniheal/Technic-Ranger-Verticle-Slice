using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorV3 : MonoBehaviour
{
    
    public GameObject MainCamera;
    public GameObject ZoomCamera;
    public GameObject righty;

    void OnTriggerEnter(Collider other)
    {
        // player.transform.position = new Vector3(0,0,0);

        MainCamera.GetComponent<SideMover>().enabled = true;

        ZoomCamera.GetComponent<Light>().enabled = true;

        Debug.Log("working");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
