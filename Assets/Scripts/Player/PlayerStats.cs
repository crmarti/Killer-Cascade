using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStats : MonoBehaviour, IDataPersistence
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

    [Header("UI")]
    public TextMeshProUGUI healthTxt;
    public TextMeshProUGUI lvlTxt;

    [Header("Position")]
    public Vector3 playerPos;
    public Vector3 playerRot;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentLevel = startingLevel;
        healthBar.SetMaxHealth(maxHealth);
        expBar.SetMaxExp(requiredExp);

        healthTxt.text = currentHealth + "/" + maxHealth;
        lvlTxt.text = "Lvl. " + currentLevel;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = this.gameObject.transform.position;
        playerRot = this.gameObject.transform.rotation.eulerAngles;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth < 0)
        {
            currentHealth = 0;
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

        healthTxt.text = currentHealth + "/" + maxHealth;
    }

    void AddExperience(int exp)
    {
        currentExp += exp;

        expBar.SetExp(currentExp);
    }

    void LevelUp()
    {
        currentLevel++;
        lvlTxt.text = "Lvl. " + currentLevel;

        currentExp = currentExp - requiredExp;
        requiredExp = CalculateRequiredXP();
        expBar.SetMaxExp(requiredExp);
        expBar.SetExp(currentExp);
        
        maxHealth += Mathf.RoundToInt((maxHealth * 0.01f) * ((100 - currentLevel) * 0.1f));
        currentHealth = maxHealth;
        healthTxt.text = currentHealth + "/" + maxHealth;
        healthBar.SetHealth(currentHealth);
        healthBar.SetMaxHealth(maxHealth);

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

    public void LoadData(SaveData data)
    {
        this.currentExp = data.currentExp;
        this.currentHealth = data.currentHealth;
        this.currentLevel = data.currentLevel;
        this.playerPos = data.position;
        this.playerRot = data.rotation;
    }

    public void SaveData(ref SaveData data)
    {
        data.currentExp = this.currentExp;
        data.currentHealth = this.currentHealth;
        data.currentLevel = this.currentLevel;
        data.position = this.playerPos;
        data.rotation = this.playerRot;
        data.levelIndex = SceneManager.GetActiveScene().buildIndex;
    }
}
