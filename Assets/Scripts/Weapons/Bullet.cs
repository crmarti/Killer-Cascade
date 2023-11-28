using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy;
    public float currentDamage;
    float baseDamage = 30f;

    PlayerStats playerStats;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
        playerStats = FindObjectOfType<PlayerStats>();

        if (playerStats.baseDamageMultiplier > 1)
        {
            currentDamage += (baseDamage * playerStats.baseDamageMultiplier);
        }
        else if (playerStats.baseDamageMultiplier == 1)
        {
            currentDamage = baseDamage;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<EnemyAI>())
        {
            EnemyAI ai = other.gameObject.GetComponent<EnemyAI>();

            ai.TakeDamage(currentDamage);
        }

        Destroy(gameObject);
    }
}
