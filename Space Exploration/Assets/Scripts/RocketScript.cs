using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public Rigidbody rb;
    public float forwardSpeed = 400f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;
    private float forwardAcceleration = 2.5f, strafeAcceleration = 2f, hoverAcceleration = 2f;

    public float lookRateSpeed = 90f;
    public Camera camera;
    private Vector2 lookInput, screenCenter, mouseDistance;

    private float rollInput;
    public float rollSpeed = 90f, rollAcceleration = 3.5f;

    public bool canMove = true;
    private Vector3 _cameraOffset;

    GameObject planet;

    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;

        Cursor.lockState = CursorLockMode.Confined;
        _cameraOffset = camera.transform.position - transform.position;
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
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
        }
        else
        {
            transform.LookAt(planet.transform, transform.up);
            transform.Rotate(90, 0, 0);
            if (Input.GetMouseButton(1))
            {
                Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X"), Vector3.up);
                _cameraOffset = camTurnAngle * _cameraOffset;
                camera.transform.LookAt(this.gameObject.transform);
                
            }
        }

        
    }



    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Planet")
        {
            /*
            FixedJoint fj = gameObject.AddComponent<FixedJoint>();
            fj.anchor = other.contacts[0].point;
            fj.connectedBody = other.rigidbody;
            rb.mass = 0.00001f;
            rb.freezeRotation = true;
            rb.velocity = new Vector3(0, 0, 0);
            */
            this.gameObject.transform.parent = other.gameObject.transform;
            planet = other.gameObject;
            canMove = false;
            //transform.position = new Vector3(0, 0, 0);
        }
    }

    /*
    public void Unstick(GameObject fj)
    {
        Destroy(fj);
        rb.mass = 1;
    }*/
}
