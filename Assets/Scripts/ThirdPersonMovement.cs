using System.Collections;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public LayerMask ground;
    public Vector3 moveDir;
    public Animator animator;

    public float speed = 6f;
    public float turnSmoothTime = 0.2f;
    public float jumpForce = 10f;
    private float gravity = 10f;
    public bool isGrounded;
    private float turnsmoothVelocity;

    // Update is called once per frame
    private void Update()
    {
        // Basic movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Checking for jump
        isGrounded = Physics.CheckSphere(transform.position, 0.2f, ground);

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isWalking", true);

            // Player rotate to face direction of movement with camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnsmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        } else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Apply gravity to our player and move them
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }

    private void Jump()
    {
        animator.SetBool("isJumping", true);

        // Applying upward force directly to player
        moveDir.y = jumpForce;
    }
}
