using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public Rigidbody rb;
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    /*
    Quaternion lookRotation;
    float rotationZ = 0;
    float mouseX = 0;
    float mouseY = 0;
    Vector3 defaultShipRotation;
    public Transform cameraPosition;
    public Camera mainCamera;
    public Transform spaceshipRoot;
    public float rotationSpeed = 2.0f;
    public float cameraSmooth = 4f;
    //public float max_acceleration = 20.0f;
    */
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAcceleration * Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookRateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, -mouseDistance.x * lookRateSpeed * Time.deltaTime, Space.Self);

        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAcceleration * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAcceleration * Time.deltaTime);
        activeHoverSpeed = Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAcceleration * Time.deltaTime);

        transform.position += transform.up * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.forward * activeHoverSpeed * Time.deltaTime);
        /*
        if (Input.GetKey(KeyCode.W))
        {
            // transform.position += transform.up * Time.deltaTime * movementSpeed;
            Vector3 v = new Vector3(0, movementSpeed, 0);
            rb.AddRelativeForce(v);
        }
        /*
        if (Input.GetKey(KeyCode.D))
        {
            // transform.position += transform.up * Time.deltaTime * movementSpeed;
            rb.velocity = new Vector3(10, 0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // transform.position += transform.up * Time.deltaTime * movementSpeed;
            rb.velocity = new Vector3(10, 0, 0);
        }*/

        /*
        if (Input.GetKey(KeyCode.W))
        {
            //Increase forward acceleration
            if (acceleration < max_acceleration)
            {
                acceleration += 0.1f;
                rb.velocity = new Vector3(0, acceleration * Time.deltaTime, 0);
                if (acceleration > 0.0f)
                {
                    print(acceleration);

                }
            }
        }
        /*
        //If the player presses S
        if (Input.GetKey(KeyCode.S))
        {
            //Accelerate in the reverse direction.
            if (acceleration < max_acceleration)
            {
                acceleration -= 0.1f;
            }
        }
        //If the player is not pressing forward or backward.
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            //Decrease acceleration.
            if (acceleration > 0.0f)
            {
                acceleration -= 0.1f;
            }
        }
        //Compensate for floating point imprecision.
        //If the player is not supposed to be moving, explicitly tell him so.
        if (acceleration > -0.05f && acceleration < 0.05f)
        {
            acceleration = 0.0f;
        }
        //Move the character in its own forward direction while taking acceleration and time into account.
        //The Time.deltaTime maintains consistent speed across all machines by syncing the speed with time.
        //Here is where the magic happens.
        //rb.velocity = new Vector3(0, acceleration * Time.deltaTime, 0);
        
        //If the player presses D
        if (Input.GetKey(KeyCode.D))
        {
            //Rotate the current game object, using deltaTime for consistency across machines.
            //transform.Rotate(transform.up, 100.0f * Time.deltaTime, Space.World);
            transform.Rotate(0.0f, 0.0f, -.2f, Space.Self);
        }
        //If the player presses A
        if (Input.GetKey(KeyCode.A))
        {
            //Rotate the current game object's transform, using deltaTim for consistency across machines.
            //transform.Rotate(transform.up, -100.0f * Time.deltaTime, Space.World);
            transform.Rotate(0.0f, 0.0f, .2f, Space.Self);
        }

        //Rotation
        
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        rigidbody.AddRelativeTorque(mouseY, 0, mouseX);
        //transform.Rotate(mouseY/2, 0.0f, mouseX/2, Space.Self);
        
        if(acceleration > 0.0f) {
            print(acceleration);
        
        }*/
    }
}
