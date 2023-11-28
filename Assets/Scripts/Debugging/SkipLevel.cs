using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLevel : MonoBehaviour
{
    GameObject player;
    PlayerStats playerStats;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerStats = player.GetComponent<PlayerStats>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            player.transform.position = new Vector3(playerStats.playerPos.x, playerStats.playerPos.y + 5, playerStats.playerPos.z);
        }
    }
}
