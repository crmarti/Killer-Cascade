using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float timeToDestroy;
    [SerializeField]
    int damage;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyAI>())
        {
            EnemyAI ai = collision.gameObject.GetComponent<EnemyAI>();

            ai.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
