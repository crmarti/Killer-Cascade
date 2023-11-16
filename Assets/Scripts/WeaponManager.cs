using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField]
    float fireRate;
    float fireRateTimer;
    float secondaryFireRate;
    float secondaryFireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    AimStateManager aim;

    // Start is called before the first frame update
    void Start()
    {
        aim = GetComponentInParent<AimStateManager>();
        fireRateTimer = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldFire())
        {
            Fire();
        } else if (ShouldSecondaryFire())
        {
            SecondaryFire();
        }
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer < fireRate)
        {
            return false;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    bool ShouldSecondaryFire()
    {
        secondaryFireRateTimer += Time.deltaTime;

        if (secondaryFireRateTimer < secondaryFireRate)
        {
            return false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }

        return false;
    }

    void Fire()
    {
        fireRateTimer = 0;
        barrelPosition.LookAt(aim.aimPos);

        GameObject currentBullet = Instantiate(bullet, barrelPosition.position + new Vector3(1, 0, 5), barrelPosition.rotation);
        Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
        rb.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
    }

    void SecondaryFire()
    {
        secondaryFireRateTimer = 0;
    }
}
