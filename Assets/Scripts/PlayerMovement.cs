using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // defining the character controller
    private CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;

    bool isGrounded;
    bool isMoving;

    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);
    // called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // update is called once per frame
    void Update()
    {
        // check if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // reset the velocity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // get the input from the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // create the movement vector
        Vector3 move = transform.right * x + transform.forward * z; // move the player

        // actually moving the player
        controller.Move(move * speed * Time.deltaTime);

        // can the player jump?
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Jumping
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // falling
        velocity.y += gravity * Time.deltaTime;

        // execute the movement
        controller.Move(velocity * Time.deltaTime);

        if (lastPosition != gameObject.transform.position && isGrounded == true)
        {
            //isMoving = true;
            // placeholder for footstep sound
        }
        else
        {
            //isMoving = false;
        }

        // update the last position
        lastPosition = gameObject.transform.position;
    }
}
