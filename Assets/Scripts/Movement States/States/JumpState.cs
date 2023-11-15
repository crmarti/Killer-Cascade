using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("isJumping", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.moveDir.magnitude > 0.1f)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                ExitState(movement, movement.Run);
            }
            else
            {
                ExitState(movement, movement.Walk);
            }
        }
        else
        {
            ExitState(movement, movement.Idle);
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("isJumping", false);
        movement.SwitchState(state);
    }
}
