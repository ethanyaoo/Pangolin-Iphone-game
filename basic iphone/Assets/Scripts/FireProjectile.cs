using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{

    // Projectile Objects
	public Rigidbody projectilePrefab;
	public Transform barrelEnd;
    public Transform firingTarget;

    // Projectile Variables
    private float firingDelayCounter; // Delay Count
    public float firingDelay = 1f;
    public int firingVelocity = 25;

    private void Start() 
    {
        firingDelayCounter = firingDelay;

    }

    void Update()
    {
        if (Input.touchCount > 0 && firingDelayCounter <= 0f)
        {

            if (firingDelayCounter <= 0f)
            {
                firingDelayCounter = firingDelay;

                Rigidbody projectileInstance;
                projectileInstance = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;

                //projectileInstance.AddForce(firingTarget.position * firingVelocity); 
                projectileInstance.AddForce(Vector3.right * firingVelocity);

            }
        }
        else
        {
            firingDelayCounter -= Time.deltaTime;
        }
    }
}
