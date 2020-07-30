using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
	public GameObject other;

    // Update is called once per frame
    void Update()
    {
        Destroy(other, 0.25f);
    }
}
