using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy;
    public float damage;

    public GameObject explosionEffect;

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

            ai.TakeDamage(damage);

            Instantiate(explosionEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
