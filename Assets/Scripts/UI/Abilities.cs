using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Rifle")]
    public Image rifle;
    public float rifleCooldown = 1f;
    bool rifleOnCooldown = false;
    
    [Header("GrenadeCD")]
    public Image grenadeAbility;
    public float grenadeCooldown = 8f;
    bool grenadeOnCooldown = false;
    public KeyCode grenade;

    [Header("DashCD")]
    public Image dashAbility;
    public float dashCooldown = 5f;
    bool dashOnCooldown = false;
    public KeyCode dash;
    
    // Start is called before the first frame update
    void Start()
    {
        rifle.fillAmount = 0;
        grenadeAbility.fillAmount = 0;
        dashAbility.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Rifle();
        Grenade();
        Dash();
    }

    void Rifle()
    {
        if (Input.GetMouseButton(0) && rifleOnCooldown == false)
        {
            rifleOnCooldown = true;
            rifle.fillAmount = 1;
        }

        if (rifleOnCooldown)
        {
            rifle.fillAmount -= 1 / rifleCooldown * Time.deltaTime;

            if (rifle.fillAmount <= 0)
            {
                rifle.fillAmount = 0;
                rifleOnCooldown = false;
            }
        }
    }

    void Grenade()
    {
        if (Input.GetKeyDown(grenade) && grenadeOnCooldown == false)
        {
            grenadeOnCooldown = true;
            grenadeAbility.fillAmount = 1;
        }

        if (grenadeOnCooldown)
        {
            grenadeAbility.fillAmount -= 1 / grenadeCooldown * Time.deltaTime;

            if (grenadeAbility.fillAmount <= 0)
            {
                grenadeAbility.fillAmount = 0;
                grenadeOnCooldown = false;
            }
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(dash) && dashOnCooldown == false)
        {
            dashOnCooldown = true;
            dashAbility.fillAmount = 1;
        }

        if (dashOnCooldown)
        {
            dashAbility.fillAmount -= 1 / dashCooldown * Time.deltaTime;

            if (dashAbility.fillAmount <= 0)
            {
                dashAbility.fillAmount = 0;
                dashOnCooldown = false;
            }
        }
    }
}
