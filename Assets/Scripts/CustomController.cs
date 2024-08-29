using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// 
/// There are 12 errors in this script!
/// Find and correct them - you have 15 minutes
/// 


public class CustomController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float crouchSpeed; 
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private LayerMask groundedMask;
    private Rigidbody rb;
    private Vector2 inputThisFrame = new Vector2();
    private Vector3 movementThisFrame = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        inputThisFrame = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputThisFrame.Normalize();

        movementThisFrame = new();

        movementThisFrame.x = inputThisFrame.x;
        movementThisFrame.z = inputThisFrame.y; 

        float speedThisFrame = walkSpeed;

        if (Input.GetButton("Fire3"))
        {
            speedThisFrame = runSpeed;
        }

        if (Input.GetButton("Fire1"))
        {
            speedThisFrame = crouchSpeed;
        }

        movementThisFrame *= speedThisFrame;

        movementThisFrame.y = rb.velocity.y - gravity * Time.deltaTime;

        if (IsGrounded())
        {
            if (Input.GetButton("Jump"))
            {
                movementThisFrame.y = jumpPower;
            }
        }

        Move(movementThisFrame);
    }

    private void Move(Vector3 direction)
    {
        rb.velocity = direction;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.001f, groundedMask);
}
}