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

    public float cameraAngleY;

    public float mouseXSensibility = 1.0f;
    public float mouseYSensibility = 1.0f;

    public bool invertY = false;

    public float topAngleY = 45.0f; // to limit the movement of the camera 
    public float botAngleY = -45.0f;

    private Rigidbody rigidBody;

    // above the start function
    // stats
    public bool isDead;
    public int health;
    public int shield;
    public int ammo;
    public int savedAmmo;
    public bool hasKey = false; // to open a door

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {

        // movement of the character
        float dir_x = Input.GetAxis("Horizontal");
        float dir_z = Input.GetAxis("Vertical");

        // rotate the camera to look
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        int invert = invertY ? -1 : 1;
        /*
        if (invertY)
        {
            mouse_y = mouse_y * -1;
        }
        else
        {
            mouse_y = mouse_y * 1;
        }*/

        cameraAngleY += mouse_y * mouseYSensibility * invert;
        cameraAngleY = Mathf.Clamp(cameraAngleY, botAngleY, topAngleY);

        Quaternion angle_mouseX = Quaternion.Euler(0.0f, mouse_x * mouseXSensibility, 0.0f);
        Quaternion angle_mouseY = Quaternion.Euler(cameraAngleY, 0.0f, 0.0f);

        transform.localRotation *= angle_mouseX; // it rotates the player only
        myCamera.transform.localRotation = angle_mouseY; // only rotates the camera

        // player controls

        // press Shift to run
        float runMultiplayer = (Input.GetAxis("Run") > 0) ? 2.0f : 1.0f; // same as the if below

        direction.x = dir_x * walkingSpeed * runMultiplayer;
        direction.z = dir_z * walkingSpeed * runMultiplayer;
        direction.y = -gravity * gravityForce;

        direction = Quaternion.FromToRotation(Vector3.forward, transform.forward) * direction;

        player.Move(direction);

        // press Space to jump
        if ((Input.GetAxis("Jump") > 0) && (isGrounded))
        {
            direction.y = jumpforce;
            if (jumpforce > 0.0f)
            {
                rigidBody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            }
            
        }

        // detects if is touching the ground
        isGrounded = player.isGrounded;

        // manual gravity
        if (!isGrounded)
        {
            direction.y -= gravity * gravityForce * Time.deltaTime;
        }

        // recover the control of the mouse when pressing esc
        if (Input.GetAxis("Cancel") > 0)
        {
            Cursor.lockState = CursorLockMode.None;
        }

        // lock the mouse again when pressing left click
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

        
} 
