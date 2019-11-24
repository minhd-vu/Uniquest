using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FauxGravityBody : MonoBehaviour
{
    public FauxGravityAttractor attractor;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        attractor.Attract(transform);
    }
}
