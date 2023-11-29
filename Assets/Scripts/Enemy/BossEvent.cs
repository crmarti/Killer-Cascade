using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private GameObject portalTrigger;
    [SerializeField]
    private GameObject portalFX;
    private BoxCollider bossTrigger;
    private GameObject spawner;

    private float spawnDistance = 100f;
    private float searchCountdown = 1f;

    private void Start()
    {
        bossTrigger = GetComponent<BoxCollider>();
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsBossAlive())
        {
            portalTrigger.SetActive(true);
            portalFX.SetActive(true);
            spawner.SetActive(true);
        }
    }

    private bool IsBossAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Boss") == null)
            {
                return false;
            }
        }

        return true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            portalTrigger.SetActive(false);
            portalFX.SetActive(false);

            Vector3 spawnPos = transform.position + transform.forward * -spawnDistance;
            Instantiate(boss, spawnPos, Quaternion.identity);
            
            bossTrigger.enabled = false;
            spawner.SetActive(false);
        }
    }
}
