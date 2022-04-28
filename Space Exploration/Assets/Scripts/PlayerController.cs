using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public Rigidbody rb;
    public float MovementSpeed = 10f;
    public float Gravity = 18f;
    public float jumpHeight = 10f;
    public float distToGround = 1f;
    public Transform groundCheck;
    public LayerMask groundMask;
    Vector3 velocity;
    public bool isGrounded;
    private float buttonHeld = 0f;
    Attractor referenceBody;
    //private bool isFalling;



    void Start()
    {
        //wings = GetComponent<AudioSource>();
    }


    void Update()
    {
        //TimerScript.Instance.UpdateTime();

        isGrounded = Physics.CheckSphere(groundCheck.position, distToGround, groundMask);
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        characterController.Move(move * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            buttonHeld = Time.time + .5f;
            Debug.Log("Jump");
            velocity.y = jumpHeight;
            characterController.Move(velocity * Time.deltaTime);
        }

        if (Input.GetButton("Jump") && !isGrounded && velocity.y < 0)
        {
            if (Time.time >= buttonHeld)
            {
                Gravity = 4.4f;
            }
        }
        else
        {
            Gravity = 18f;
        }

        // Gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;

        }

        velocity.y -= Gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);


    }


    /*void FixedUpdate()
    {
        Attractor[] bodies = FindObjectsOfType<Attractor>();
        Vector3 strongestGravitionalPull = Vector3.zero;

        // Gravity
        foreach (Attractor body in bodies)
        {
            float sqrDst = (body.transform.position - rb.position).sqrMagnitude;
            Vector3 forceDir = (body.transform.position - rb.position).normalized;
            Vector3 acceleration = forceDir * 667.4f * body.mass / sqrDst;
            rb.AddForce(acceleration, ForceMode.Acceleration);

            // Find body with strongest gravitational pull 
            if (acceleration.sqrMagnitude > strongestGravitionalPull.sqrMagnitude)
            {
                strongestGravitionalPull = acceleration;
                referenceBody = body;
            }
        }

        // Rotate to align with gravity up
        Vector3 gravityUp = -strongestGravitionalPull.normalized;
        rb.rotation = Quaternion.FromToRotation(transform.up, gravityUp) * rb.rotation;

        // Move
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }*/
}