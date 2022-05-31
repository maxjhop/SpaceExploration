
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    // horizontal rotation speed
    public float horizontalSpeed = 1000f;
    // vertical rotation speed
    public float verticalSpeed = 1000f;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    //private Camera cam;
    public Transform playerBody;
    public GameObject cam;


    void Start()
    {
        //cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }



    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSpeed * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.eulerAngles = new Vector3(xRotation, yRotation, 0.0f);
    }
}