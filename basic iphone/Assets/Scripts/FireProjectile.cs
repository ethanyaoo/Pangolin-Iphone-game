using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
	public Rigidbody projectilePrefab;
	public Transform barrelEnd;

    private float firingDelay = 0f;

    public float firingVelocity = 200f;
    
    public Transform rotationCenter;

    public Transform firingTarget;

    public PipeSystem pipeSystem;

    // Issue with firing is that firingTarget appears outside of tunnel and 
    //  will occassionally cause issue of projectileInstance being launched outside
    void Update()
    {
        if (Input.touchCount > 0)
        {
            print("FIRING " + firingDelay);
            firingDelay -= Time.deltaTime;

            if (firingDelay <= 0f)
            {
                firingDelay = 0.5f;

                Rigidbody projectileInstance;
                projectileInstance = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;

                //projectileInstance.AddForce(pipeSystem.CurrPipe.transform.position * 15);
                projectileInstance.AddForce(firingTarget.position * 10); 
            }
        }
        else
        {
            firingDelay = 0f;
        }
    }
}
