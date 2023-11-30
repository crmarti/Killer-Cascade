using Scene_Teleportation_Kit.Scripts.teleport;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalTeleport : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        Teleportable teleportable = collider.GetComponent<Teleportable>();
        if (teleportable != null)
        {
            Destroy(collider.gameObject);
            Time.timeScale = 0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
