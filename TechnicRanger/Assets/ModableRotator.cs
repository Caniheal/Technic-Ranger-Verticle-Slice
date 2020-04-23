using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModableRotator : MonoBehaviour
{
    public int xAngle, yAngle, zAngle;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(xAngle, yAngle, zAngle) * Time.deltaTime);
    }
}
