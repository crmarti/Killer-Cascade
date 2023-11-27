using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState
    {
        SPAWNING,
        WAITING,
        COUNTING
    };

    [Serializable]
    public class Wave
    {
        public Transform enemy;
        public string name;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    public Transform player;
    private SpawnState state = SpawnState.COUNTING;

    private int nextWave = 0;
    public float timeBetweenWaves = 10f;
    private float waveCountdown;
    private float searchCountdown = 1f;
    private float minDistance = 20f;
    private float maxDistance = 40f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            // Check if any enemies are still alive or a set time has passed
            if (!IsEnemyAlive() || waveCountdown <= 0)
            {
                // Begin a new enemy wave
                WaveCompleted();
            } 
            else
            {
                return;
            }
        }
        
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                // Start spawning enemies
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        } 
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    private void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning enemy: " +  _enemy.name);

        float distance = UnityEngine.Random.Range(minDistance, maxDistance);
        float angle = UnityEngine.Random.Range(-Mathf.PI, Mathf.PI);

        Vector3 spawnPos = player.position;
        spawnPos += new Vector3(Mathf.Cos(angle) * distance, -player.position.y, Mathf.Sin(angle) * distance);

        Instantiate(_enemy, spawnPos, Quaternion.identity);
    }

    private void WaveCompleted()
    {
        Debug.Log("Wave Destroyed!");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Restarting spawn loop");
        }
        else
        {
            nextWave++;
        }
    }

    private bool IsEnemyAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }

        return true;
    }
}
