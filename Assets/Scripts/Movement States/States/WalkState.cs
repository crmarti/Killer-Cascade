using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("isWalking", true);
        movement.currentMoveSpeed = movement.walkSpeed;
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ExitState(movement, movement.Crouch);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            ExitState(movement, movement.Jump);
        }
        else if (movement.moveDir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("isWalking", false);
        movement.SwitchState(state);
    }
}
