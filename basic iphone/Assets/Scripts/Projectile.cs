using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform firingTarget;
<<<<<<< HEAD

    public ParticleSystem breakEffect;
=======
>>>>>>> 65ab40b6c91b266d820b05d247595b7782c49ebc

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.SetActive(false);
        gameObject.SetActive(false);
<<<<<<< HEAD

        Instantiate(breakEffect, collider.transform.position, Quaternion.identity);
=======
>>>>>>> 65ab40b6c91b266d820b05d247595b7782c49ebc
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = firingTarget.localRotation;
    }
}
