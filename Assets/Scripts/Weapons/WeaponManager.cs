using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Fire Rate")]
    [SerializeField]
    float fireRate;
    float fireRateTimer;
    [SerializeField]
    float secondaryFireRate;
    float secondaryFireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPosition;
    [SerializeField] float bulletVelocity;
    [SerializeField] int bulletsPerShot;
    
    AimStateManager aim;

    [Header("Grenade Properties")]
    [SerializeField] GameObject grenade;
    [SerializeField] float grenadeVelocity;

    [Header("Sounds")]
    [SerializeField] AudioClip gunShot;
    [SerializeField] AudioClip secondaryShot;
    AudioSource audioSource;

    WeaponBloom bloom;
    Light muzzleFlashLight;
    float lightIntensity;
    [Header("VFX")]
    [SerializeField] float lightReturnSpeed = 20;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        aim = GetComponentInParent<AimStateManager>();
        muzzleFlashLight = GetComponentInChildren<Light>();
        bloom = GetComponent<WeaponBloom>();
        lightIntensity = muzzleFlashLight.intensity;
        fireRateTimer = fireRate;
        secondaryFireRateTimer = secondaryFireRate;
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

        muzzleFlashLight.intensity = Mathf.Lerp(muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
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
        barrelPosition.LookAt(aim.actualAimPosition);
        barrelPosition.localEulerAngles = bloom.generateBloomAngle(barrelPosition);

        audioSource.PlayOneShot(gunShot);
        TriggerMuzzleFlash();

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject currentBullet = Instantiate(bullet, barrelPosition.position, barrelPosition.rotation);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
        }
    }

    void SecondaryFire()
    {
        secondaryFireRateTimer = 0;
        barrelPosition.LookAt(aim.actualAimPosition);

        audioSource.PlayOneShot(secondaryShot);

        GameObject currentGrenade = Instantiate(grenade, barrelPosition.position, barrelPosition.rotation);
        Rigidbody rb = currentGrenade.GetComponent<Rigidbody>();
        rb.AddForce(barrelPosition.forward * grenadeVelocity, ForceMode.Impulse);
    }

    void TriggerMuzzleFlash()
    {
        muzzleFlashLight.intensity = lightIntensity;
    }
}
