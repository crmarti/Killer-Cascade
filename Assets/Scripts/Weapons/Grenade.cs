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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            soundManager.Play("GrenadeExplosion");
            EnemyAI ai = collision.gameObject.GetComponent<EnemyAI>();

            ai.TakeDamage(currentDamage);

            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
