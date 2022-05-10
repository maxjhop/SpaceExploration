// code inspired by https://www.youtube.com/watch?v=Ouu3D_VHx9o
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 667.4f;
    public Rigidbody rb;
    public Vector3 initialVelocity;
    public float mass;
    
    void Start()
    {
        //give it the direction you want as before;
        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;
        mass = rb.mass;
        print(mass);
        
    }

    void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();
        foreach (Attractor attractor in attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }

        }

    }

    void Attract(Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);
    }
}

