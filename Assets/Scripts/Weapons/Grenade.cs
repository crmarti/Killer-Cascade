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

    private void Start()
    {
        stats = FindObjectOfType<PlayerStats>();

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
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            EnemyAI ai = collision.gameObject.GetComponent<EnemyAI>();

            ai.TakeDamage(currentDamage);

            Instantiate(explosionEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
