using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("isCrouching", true);
        movement.currentMoveSpeed = movement.crouchSpeed;
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ExitState(movement, movement.Run);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            if (movement.moveDir.magnitude < 0.1f)
            {
                ExitState(movement, movement.Idle);
            }
            else
            {
                ExitState(movement, movement.Walk);
            }
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("isCrouching", false);
        movement.SwitchState(state);
    }
}
