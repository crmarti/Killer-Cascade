using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    private int level;
    private int currentHealth;
    private int currentExp;
    private int levelIndex;

    private float[] position;

    public SaveData(Transform player, PlayerStats stats)
    {
        level = stats.currentLevel;
        currentHealth = stats.currentHealth;
        currentExp = stats.currentExp;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        
        levelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public int getLevel()
    {
        return level;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getCurrentExp()
    {
        return currentExp;
    }
    
    public int getLevelIndex()
    {
        return levelIndex;
    }

    public Vector3 getPosition()
    {
        return new Vector3(position[0], position[1], position[2]);
    }
}
