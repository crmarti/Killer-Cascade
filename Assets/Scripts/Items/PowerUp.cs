using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpTypes
    {
        HEALING,
        DAMAGEBOOST,
        MOVEMENTBOOST
    };

    public GameObject pickupEffect;
    public PowerUpTypes powerUpType;

    Bullet bullet;
    Grenade grenade;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        switch(powerUpType)
        {
            case PowerUpTypes.HEALING:
                Instantiate(pickupEffect, transform.position, transform.rotation);

                stats.health += 30;

                Destroy(gameObject);
                break;

            case PowerUpTypes.DAMAGEBOOST:
                Instantiate(pickupEffect, transform.position, transform.rotation);

                foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.enabled = false;
                }
                GetComponent<Collider>().enabled = false;

                stats.baseDamageMultiplier *= 1.5f;
                
                yield return new WaitForSeconds(5f);

                stats.baseDamageMultiplier /= 1.5f;

                Destroy(gameObject);
                break;

            case PowerUpTypes.MOVEMENTBOOST:
                Instantiate(pickupEffect, transform.position, transform.rotation);

                break;
            
            default:
                Debug.Log("Invalid pickup type!");
                break;
        }
    }
}
