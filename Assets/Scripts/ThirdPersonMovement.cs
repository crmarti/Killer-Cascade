using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public LayerMask ground;
    private Rigidbody rb;

    public float speed = 6f;
    public float turnSmoothTime = 0.2f;
    public float jumpForce = 10f;
    public float dashForce = 30f;
    public float dashCooldown = 0f;
    private bool isGrounded;
    private float turnsmoothVelocity;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Basic movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            // Player rotate to face direction of movement with camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnsmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        // Check if the player is able to jump
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, 1.9f, 0), 0.1f, ground);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Check if the player can dash
        if (CanDash() && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void Jump()
    {
        // Applying upward force directly to player
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Dash()
    {
        // Applying forward force directly to player
        rb.AddForce(new Vector3(1f, 0f, 1f) * dashForce, ForceMode.Impulse);

        dashCooldown = 5f;
    }

    private bool CanDash()
    {
        if (dashCooldown <= 0)
        { 
            return true;
        } else
        {
            dashCooldown -= Time.deltaTime;
            return false;
        }
    }
}
