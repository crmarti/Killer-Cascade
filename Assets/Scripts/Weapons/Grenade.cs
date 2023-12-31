using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy;
    public float currentDamage;
    float baseDamage = 100f;

    PlayerStats stats;
    public GameObject explosionEffect;
    private SoundManager soundManager;

    private void Start()
    {
        stats = FindObjectOfType<PlayerStats>();
        soundManager = FindObjectOfType<SoundManager>();

        if (stats.baseDamageMultiplier > 1)
        {
            currentDamage += (baseDamage * stats.baseDamageMultiplier);
        }
        else if (stats.baseDamageMultiplier == 1)
        {
            currentDamage = baseDamage;
        }
    }

    private void Update()
    {
        timeToDestroy -= Time.deltaTime;

        if (timeToDestroy <= 0)
        {
            soundManager.Play("GrenadeExplosion");
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            soundManager.Play("GrenadeExplosion");
            EnemyAI ai = other.gameObject.GetComponent<EnemyAI>();

            ai.TakeDamage(currentDamage);

            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.GetComponent<FlyingAI>()) 
        {
            soundManager.Play("GrenadeExplosion");
            FlyingAI ai = other.gameObject.GetComponent<FlyingAI>();

            ai.TakeDamage(currentDamage);

            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.gameObject.GetComponent<BossAI>())
        {
            soundManager.Play("GrenadeExplosion");
            BossAI ai = other.gameObject.GetComponent<BossAI>();

            ai.TakeDamage(currentDamage);

            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
