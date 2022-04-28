using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocity : MonoBehaviour
{
    Rigidbody rb;
    Vector3 speed;
    void Start()
    {
        //give it the direction you want as before;
        rb = GetComponent<Rigidbody>();
        speed = new Vector3(75, 0, 0);
        rb.velocity = speed;
    }
}
