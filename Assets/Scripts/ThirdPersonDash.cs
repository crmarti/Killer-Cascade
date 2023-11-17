using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonDash : MonoBehaviour
{
    private MovementStateManager moveScript;
    private CharacterController controller;

    public float dashSpeed;
    public float dashTime;
    private float dashCooldown = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        moveScript = GetComponent<MovementStateManager>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CanDash() && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Dash());

            dashCooldown = 5f;
        }
    }

    IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private bool CanDash()
    {
        if (dashCooldown <= 0)
        {
            return true;
        }
        else
        {
            dashCooldown -= Time.deltaTime;
            return false;
        }
    }
}
