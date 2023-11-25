using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SaveData
{
    public int currentLevel;
    public int currentHealth;
    public int currentExp;
    public int levelIndex;

    public Vector3 position;
    public Vector3 rotation;

    public SaveData()
    {
        currentLevel = 1;
        currentHealth = 100;
        currentExp = 0;

        position = new Vector3(58, 1.1f, 82);
        rotation = new Vector3(0, -164.017f, 0);
        
        levelIndex = 1;
    }
}
