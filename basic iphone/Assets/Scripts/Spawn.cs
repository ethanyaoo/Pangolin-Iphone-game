using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
	public Rigidbody wallPrefab;
	public Rigidbody powerupPrefab;
	public Rigidbody consumablePrefab;

	public Transform spawnLocation;

	public float randomVal;

    // Start is called before the first frame update
    void Start()
    {
    	InvokeRepeating("SpawnObjects", 1, 1);
    }

    void SpawnObjects()
    {
    	randomVal = Random.Range(0.0f, 100.0f);
    	print(randomVal);

    	if (randomVal < 25.0f)
    	{
    		Rigidbody spawnObjectInstance;
        	spawnObjectInstance = Instantiate(wallPrefab, spawnLocation.position, spawnLocation.rotation) as Rigidbody;
        	spawnObjectInstance.AddForce(-transform.forward * 5);
    	}
    	else if (randomVal > 25.0f && randomVal < 45.0f)
    	{
    		Rigidbody spawnObjectInstance;
        	spawnObjectInstance = Instantiate(consumablePrefab, spawnLocation.position, spawnLocation.rotation) as Rigidbody;
        	spawnObjectInstance.AddForce(-transform.forward * 5);
    	}
    	else if (randomVal > 95.0f)
    	{
    		Rigidbody spawnObjectInstance;
        	spawnObjectInstance = Instantiate(powerupPrefab, spawnLocation.position, spawnLocation.rotation) as Rigidbody;
        	spawnObjectInstance.AddForce(-transform.forward * 5);
    	}
    }
}
