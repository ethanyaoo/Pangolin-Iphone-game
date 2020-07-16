using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform firingTarget;

    public ParticleSystem breakEffect;
    ParticleSystem.MainModule breakMain;

    private void Start() 
    {
        ParticleSystem.MainModule breakMain = breakEffect.main;
    }

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        collider.gameObject.SetActive(false);
        gameObject.SetActive(false);

        breakMain.startColor = new Color(
                                            (float)Random.Range(0, 255),
                                            (float)Random.Range(0, 255),
                                            (float)Random.Range(0, 255)
        );

        Instantiate(breakEffect, collider.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localRotation = firingTarget.localRotation;
    }
}
