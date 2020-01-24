using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyPlayer : MonoBehaviour
{
    public Animation walkingAnimation;
    public float walkSpeed = 0.5f;
    public float jumpHeight = 2f;
    public bool isGrounded = false;
    public bool airControl = true;

    private float distToGround = 0.2f;
    private Rigidbody body;
    private Vector3 direction;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround);
    }

    private void FixedUpdate()
    {
        Movement();

        if (direction != Vector3.zero)
        {
            HandleRotation();
        }
    }

    public void Movement()
    {
        // Calculate direction vector for movement
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        direction = (new Vector3(h, 0, v)).normalized;

        // Check that player is grounded
        isGrounded = IsGrounded();

        if (IsGrounded())
        {
            // Handle jump
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            // Can always move directionally on the ground
            DirectionalMovement();
        }

        // Can move directionally regardless of grounding
        if (airControl)
            DirectionalMovement();

        // Play walking animation if moving
        if (body.velocity.magnitude > 1 && !walkingAnimation.isPlaying)
            walkingAnimation.Play();
    }

    public void DirectionalMovement()
    {
        // Convert direction from local to world relative to camera
        Vector3 converted = Camera.main.transform.TransformDirection(direction) * (walkSpeed * 100) * Time.deltaTime;
        body.velocity = new Vector3(converted.x, body.velocity.y, converted.z);
    }

    private void Jump()
    {
        body.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    public void HandleRotation()
    {
        float targetRotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        Quaternion lookAt = Quaternion.Slerp(transform.rotation,
                                      Quaternion.Euler(0, targetRotation, 0),
                                      0.5f);
        body.rotation = lookAt;

    }
}