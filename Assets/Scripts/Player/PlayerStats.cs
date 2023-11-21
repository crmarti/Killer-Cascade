using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int currentHealth;
    int maxHealth = 100;
    
    public int currentExp;
    int maxExp = 100;
    int startingLevel = 1;
    public int currentLevel;

    public HealthBar healthBar;
    public ExperienceBar expBar;

    public float baseDamageMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLevel = startingLevel;
        healthBar.SetMaxHealth(maxHealth);
        expBar.SetMaxExp(maxExp * currentLevel);
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
            AddExperience(20);
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
}
