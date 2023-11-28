using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    CharacterController controller;
    [HideInInspector] public Vector3 moveDir;

    // Movement
    [HideInInspector]
    public float hzInput, vInput;
    public float currentMoveSpeed;
    public float walkSpeed = 8f;
    public float runSpeed = 12f;
    public float crouchSpeed = 4f;
    public float airSpeed = 4.5f;
    
    // Ground check
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    // Gravity
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpForce;
    [HideInInspector] public bool jumped;
    Vector3 velocity;

    // State Machine
    public MovementBaseState currentState;
    public MovementBaseState previousState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public CrouchState Crouch = new CrouchState();
    public RunState Run = new RunState();
    public JumpState Jump = new JumpState();

    [HideInInspector]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();
        Falling();

        animator.SetFloat("hzInput", hzInput);
        animator.SetFloat("vInput", vInput);

        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");
        
        Vector3 airDirection = Vector3.zero;
        if (!IsGrounded()) airDirection = transform.forward * vInput + transform.right * hzInput;
        else moveDir = transform.forward * vInput + transform.right * hzInput;

        controller.Move((moveDir.normalized * currentMoveSpeed + airDirection.normalized * airSpeed) * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);

        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask))
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    void Gravity()
    {
        if (!IsGrounded())
        {
            velocity.y += gravity * Time.deltaTime;
        } 
        else if (velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void Falling() => animator.SetBool("isFalling", !IsGrounded());

    public void JumpForce() => velocity.y += jumpForce;

    public void Jumped() => jumped = true;

    public void TeleportTo(Transform destination)
    {
        transform.position = destination.position;
    }
}
