using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhyloTunnel : MonoBehaviour
{
    public Transform _tunnel;
    public float ampBuffer;
    public float _tunnelSpeed, _cameraDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _tunnel.position = new Vector3(_tunnel.position.x, _tunnel.position.y, _tunnel.position.z + (ampBuffer * _tunnelSpeed));

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, _tunnel.position.z + _cameraDistance);
    }
}
