using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDestroy : MonoBehaviour
{
    public ParticleSystem ps;

    public float timeDestroy = 1f;

    private void Start() 
    {
        //ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        var main = ps.main;

        // main.startColor = new ParticleSystem.MinMaxGradient(new Color(
        //                                                 (float)Random.Range(0, 255),
        //                                                 (float)Random.Range(0, 255),
        //                                                 (float)Random.Range(0, 255)
        // ));
        main.startColor = new ParticleSystem.MinMaxGradient(Color.red);

        var randVal = Random.Range(0, 4);

        switch(randVal)
        {
            case 1:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.red);
                break;
            case 2:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow);
                break;
            case 3:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.blue);
                break;
            case 4:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.green);
                break;
            default:
                main.startColor = new ParticleSystem.MinMaxGradient(Color.cyan);
                break;
        }
    }

    private void Update() 
    {
        timeDestroy -= Time.deltaTime;

        if (timeDestroy <= 0)
        {
            Destroy(gameObject, 0f);
        }
    }
}
