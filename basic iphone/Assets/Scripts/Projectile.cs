using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform firingTarget;

    //public ParticleSystem breakEffect;
    public GameObject breakEffect;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        gameObject.SetActive(false);

        if (collider.tag == "Collisions")
        {
            collider.gameObject.SetActive(false);

            Instantiate(breakEffect, collider.transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = firingTarget.localRotation;
    }
}
