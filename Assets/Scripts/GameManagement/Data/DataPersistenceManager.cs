using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField]
    private bool initializeDataIfNull = false;

    [Header("File Storage Config")]
    [SerializeField]
    private string fileName;
    [SerializeField]
    private bool useEncryption;

    public static DataPersistenceManager instance { get; private set;}

    private SaveData saveData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Data Persistence instance in the scene! Destroying newest instance.");
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
    }

    public void NewGame()
    {
        this.saveData = new SaveData();
    }

    public void LoadGame()
    {
        this.saveData = dataHandler.Load();
        
        if (this.saveData == null && initializeDataIfNull)
        {
            NewGame();
        }

        if (this.saveData == null)
        {
            Debug.Log("No save data found. A new game needs to be started before data can be loaded.");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(saveData);
        }
    }

    public void SaveGame()
    {
        if (this.saveData == null)
        {
            Debug.LogWarning("No save data found. A new game needs to be started before data can be loaded.");
            return;
        }
        
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref saveData);
        }

        dataHandler.Save(saveData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
    
    public bool HasGameData()
    {
        return this.saveData != null;
    }

    public SaveData getSaveData()
    {
        return this.saveData;
    }
}
