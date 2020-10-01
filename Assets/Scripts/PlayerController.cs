using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private CharacterController player;

    public Vector3 direction = Vector3.zero; // the direction the player is looking

    public float walkingSpeed; // the speed when it walks
    public float jumpforce;

    public float gravity = 9.81f; // manual gravity
    public float gravityForce;

    public bool isGrounded;

    float runMultiplayer;

    public Camera myCamera;

    public float mouseXSensibility = 1.0f;
    public float mouseYSensibility = 1.0f;

    public bool invertY = false;

    public float topAngleY = 45.0f; // to limit the movement of the camera 
    public float botAngleY = -45.0f;

    private Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate ()
    {
        // rotate the camera to look
        float mouse_x = Input.GetAxis("Mouse X") * mouseXSensibility * Time.fixedDeltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * mouseYSensibility * Time.fixedDeltaTime;

        mouse_y = Mathf.Clamp(mouse_y, botAngleY, topAngleY);

        // invert if activated the invert part
        mouse_y = invertY ? mouse_y * -1 : mouse_y * 1;

        /*
        if (invertY)
        {
            mouse_y = mouse_y * -1;
        }
        else
        {
            mouse_y = mouse_y * 1;
        }*/

        transform.Rotate(0, mouse_x, 0);
        myCamera.transform.Rotate(mouse_y, 0, 0);

        // movement of the character
        float dir_z = Input.GetAxis("Vertical");
        float dir_x = Input.GetAxis("Horizontal");

        // press Shift to run
        float runMultiplayer = (Input.GetAxis("Run") > 0) ? 2.0f : 1.0f; // same as the if below

        direction.x = dir_x * walkingSpeed * runMultiplayer * Time.deltaTime;
        direction.z = dir_z * walkingSpeed * runMultiplayer * Time.deltaTime;

        player.Move(direction);

        // press Space to jump
        if ((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            rigidBody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }

        // manual gravity
    }
}
