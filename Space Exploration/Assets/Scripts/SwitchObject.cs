using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : MonoBehaviour
{

    public GameObject ship;
    public GameObject player;
    public RocketScript rs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F) && !(rs.canMove) )
        {
            player.transform.position = ship.transform.position;    
            ship.SetActive(false);  
            player.SetActive(true);
        }
    }
}
