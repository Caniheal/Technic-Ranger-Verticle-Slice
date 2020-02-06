using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Rigidbody projectilePrefab;
    [SerializeField]
    private float launchForce = 700f;


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            LaunchProjectile();
        }


    }

    private void LaunchProjectile()
    {

        var projectileInstance = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
            other.GetComponent<Animation>().enabled = true;
    }

}
