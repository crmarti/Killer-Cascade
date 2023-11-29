using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public List<GameObject> crates;
    public LayerMask isGround, isPlayer;
    public GameObject explosionEffect;

    // General
    public float health;
    public int damage;
    public int experience;

    // For patrolling
    public Vector3 walkPoint;
    [SerializeField]
    bool walkPointSet;
    public float walkPointRange;

    // For Attacking
    public float timeBetweenAttacks;
    public bool hasAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool isDead;

    private float spawnDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Check for sight and/or attack ranges
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patrolling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchForWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Did we reach the walkpoint?
        if (distanceToWalkPoint.magnitude < 0.5f)
        {
            walkPointSet = false;
        }
    }

    private void SearchForWalkPoint()
    {
        // Calculate a point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, isGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    private void AttackPlayer()
    {
        // Enemy doesn't move while attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);

        if (!hasAttacked)
        {
            // Attacking
            hasAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            Debug.Log("hit");
        }

        if (health <= 0)
        {
            isDead = true;
            sightRange = 0;
            attackRange = 0;
            Invoke(nameof(DestroyEnemy), 1f);
        }
    }

    public void DestroyEnemy()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.AddExperience(experience);

        int spawnChance = Random.Range(1, 100);
        SpawnCrate(spawnChance);

        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void SpawnCrate(int spawnChance)
    {
        Vector3 spawnPos = transform.position + transform.forward * spawnDistance;

        if (spawnChance <= 18)
        {
            Instantiate(crates[0], spawnPos, Quaternion.identity);
        }
        else if (spawnChance > 18 && spawnChance <= 28)
        {
            Instantiate(crates[1], spawnPos, Quaternion.identity);
        }
        else if (spawnChance > 28 && spawnChance <= 38)
        {
            Instantiate(crates[2], spawnPos, Quaternion.identity);
        }
    }
}
