using System.Collections;
using System.Collections.Generic;
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
        switch(powerUpType)
        {
            case PowerUpTypes.HEALING:
                Instantiate(pickupEffect, transform.position, transform.rotation);

                PlayerStats stats = player.GetComponent<PlayerStats>();
                stats.health += 30;

                Destroy(gameObject);
                break;

            case PowerUpTypes.DAMAGEBOOST:
                bullet = FindObjectOfType<Bullet>();
                grenade = FindObjectOfType<Grenade>();
                
                Instantiate(pickupEffect, transform.position, transform.rotation);

                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                bullet.damage *= 1.5f;
                grenade.damage *= 1.5f;

                yield return new WaitForSeconds(3f);

                bullet.damage /= 1.5f;
                grenade.damage /= 1.5f;

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
