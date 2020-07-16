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

    // Audio Controls
    private AudioSource audioSource;
    private void Start() 
    {
        firingDelayCounter = firingDelay;

        audioSource = GetComponent<AudioSource>();
    }

    // Issue with firing is that firingTarget appears outside of tunnel and 
    //  will occassionally cause issue of projectileInstance being launched outside
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

                audioSource.Play();
            }
        }
        else
        {
            firingDelayCounter -= Time.deltaTime;
        }
    }
}
