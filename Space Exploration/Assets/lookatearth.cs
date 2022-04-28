using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookatearth : MonoBehaviour
{
    public Attractor planet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(planet.transform);
    }
}
