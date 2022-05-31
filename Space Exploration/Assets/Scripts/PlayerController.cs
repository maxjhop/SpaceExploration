using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public LayerMask walkableMask;

    public float maxAcceleration;
    public Transform feet;
    public float walkSpeed = 8;
    public float runSpeed = 14;
    public float jumpForce = 20;
    public float vSmoothTime = 0.1f;
    public float airSmoothTime = 0.5f;
    public float stickToGroundForce = 8;

    public bool lockCursor;
    public float mass = 70;
    Rigidbody rb;
    public GameObject spaceship;

    public float mouseSensitivity = 10;
    public Vector2 pitchMinMax = new Vector2(-40, 85);
    public float rotationSmoothTime = 0.1f;



    public Vector3 targetVelocity;
    Vector3 cameraLocalPos;
    Vector3 smoothVelocity;
    Vector3 smoothVRef;

    Attractor referenceBody;

    Camera cam;
    bool readyToFlyShip;
    public Vector3 delta;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        cameraLocalPos = cam.transform.localPosition;
        InitRigidbody();

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void InitRigidbody()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.useGravity = false;
        rb.isKinematic = false;
        rb.mass = mass;
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        // Movement
        bool isGrounded = IsGrounded();
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        targetVelocity = transform.TransformDirection(input.normalized) * currentSpeed;
        smoothVelocity = Vector3.SmoothDamp(smoothVelocity, targetVelocity, ref smoothVRef, (isGrounded) ? vSmoothTime : airSmoothTime);

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
                isGrounded = false;
            }
            else
            {
                // Apply small downward force to prevent player from bouncing when going down slopes
                rb.AddForce(-transform.up * stickToGroundForce, ForceMode.VelocityChange);
            }
        }
    }

    bool IsGrounded()
    {
        // Sphere must not overlay terrain at origin otherwise no collision will be detected
        // so rayRadius should not be larger than controller's capsule collider radius
        const float rayRadius = .3f;
        const float groundedRayDst = .2f;
        bool grounded = false;

        if (referenceBody)
        {
            var relativeVelocity = rb.velocity - (referenceBody.GetComponent<Rigidbody>()).velocity;
            // Don't cast ray down if player is jumping up from surface
            if (relativeVelocity.y <= jumpForce * .5f)
            {
                RaycastHit hit;
                Vector3 offsetToFeet = (feet.position - transform.position);
                Vector3 rayOrigin = rb.position + offsetToFeet + transform.up * rayRadius;
                Vector3 rayDir = -transform.up;

                grounded = Physics.SphereCast(rayOrigin, rayRadius, rayDir, out hit, groundedRayDst, walkableMask);
            }
        }

        return grounded;
    }

    void FixedUpdate()
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
        rb.MovePosition(rb.position + smoothVelocity * Time.fixedDeltaTime);
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }


    public Camera Camera
    {
        get
        {
            return cam;
        }
    }

    public Rigidbody Rigidbody
    {
        get
        {
            return rb;
        }
    }

}