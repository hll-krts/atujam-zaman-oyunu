using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OtherMovement : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {   
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //rb.AddRelativeForce(rb.velocity);
    }
}
