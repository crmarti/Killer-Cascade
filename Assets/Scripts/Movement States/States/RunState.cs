using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.animator.SetBool("isRunning", true);
        movement.currentMoveSpeed = movement.runSpeed;
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
           ExitState(movement, movement.Walk);
        } 
        else if (movement.moveDir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.animator.SetBool("isRunning", false);
        movement.SwitchState(state);
    }
}
