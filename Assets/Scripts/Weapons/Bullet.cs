using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy;
    public float currentDamage;
    float baseDamage = 25f;

    PlayerStats playerStats;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
        playerStats = FindObjectOfType<PlayerStats>();
        currentDamage = baseDamage;
    }

    private void Update()
    {
        if (playerStats.baseDamageMultiplier > 1)
        {
            currentDamage *= playerStats.baseDamageMultiplier;
        }
        else if (playerStats.baseDamageMultiplier == 1)
        {
            currentDamage = baseDamage;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            EnemyAI ai = collision.gameObject.GetComponent<EnemyAI>();

            ai.TakeDamage(currentDamage);
        }

        Destroy(gameObject);
    }
}
