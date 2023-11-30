using System.Collections;
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
    private SoundManager soundManager;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
    }

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
                soundManager.Play("ItemPickup");

                stats.currentHealth += 30;

                Destroy(gameObject);
                break;

            case PowerUpTypes.DAMAGEBOOST:
                Instantiate(pickupEffect, transform.position, transform.rotation);
                soundManager.Play("ItemPickup");

                foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.enabled = false;
                }
                GetComponent<Collider>().enabled = false;

                stats.baseDamageMultiplier *= 1.5f;
                
                yield return new WaitForSeconds(10f);

                stats.baseDamageMultiplier /= 1.5f;

                Destroy(gameObject);
                break;

            case PowerUpTypes.MOVEMENTBOOST:
                Instantiate(pickupEffect, transform.position, transform.rotation);
                soundManager.Play("ItemPickup");

                foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.enabled = false;
                }
                GetComponent<Collider>().enabled = false;

                player.GetComponent<MovementStateManager>().walkSpeed *= 1.5f;
                player.GetComponent<MovementStateManager>().runSpeed *= 1.5f;
                player.GetComponent<MovementStateManager>().crouchSpeed *= 1.5f;

                yield return new WaitForSeconds(6f);

                player.GetComponent<MovementStateManager>().walkSpeed /= 1.5f;
                player.GetComponent<MovementStateManager>().runSpeed /= 1.5f;
                player.GetComponent<MovementStateManager>().crouchSpeed /= 1.5f;

                break;
            
            default:
                Debug.Log("Invalid pickup type!");
                break;
        }
    }
}
