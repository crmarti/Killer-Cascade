using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBar;
    public ExperienceBar expBar;

    [Header("Health")]
    public int currentHealth;
    int maxHealth = 100;
    
    [Header("Experience & Leveling")]
    public int currentExp;
    public int requiredExp = 120;
    int startingLevel = 1;
    int maxLevel = 15;
    public int currentLevel;
    public float additionMultiplier = 300;
    public float powerMultiplier = 2;
    public float divisionMultiplier = 7;

    [Header("Damage")]
    public float baseDamageMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLevel = startingLevel;
        healthBar.SetMaxHealth(maxHealth);
        expBar.SetMaxExp(requiredExp);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(20);
        }
        else if (Input.GetKeyDown(KeyCode.M)) 
        {
            AddExperience(35);
        }

        if (currentExp >= requiredExp && currentLevel != maxLevel)
        {
            LevelUp();
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

    void AddExperience(int exp)
    {
        currentExp += exp;

        expBar.SetExp(currentExp);
    }

    void LevelUp()
    {
        currentLevel++;

        currentExp = currentExp - requiredExp;
        requiredExp = CalculateRequiredXP();
        expBar.SetMaxExp(requiredExp);
        expBar.SetExp(currentExp);
        
        maxHealth += Mathf.RoundToInt((maxHealth * 0.01f) * ((100 - currentLevel) * 0.1f));
        currentHealth = maxHealth;

        baseDamageMultiplier += (0.01f * currentLevel);
    }

    private int CalculateRequiredXP()
    {
        int solveForRequiredXp = 0;

        for (int levelCycle = 1; levelCycle <= currentLevel; levelCycle++)
        {
            solveForRequiredXp += (int) Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }

        return solveForRequiredXp / 4;
    }
}
