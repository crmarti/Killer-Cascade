using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject player;
    public LayerMask isGround, isPlayer;
    private Animator animator;

    // General
    public float health;
    public int damage;
    public int experience;

    // For Attacking
    public float timeBetweenAttacks;
    public bool hasAttacked;

    // States
    private float sightRange = float.PositiveInfinity;
    public float attackRange;
    public bool playerInSightRange, playerInAttackRange;
    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
        animator.SetBool("IsMoving", true);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player.transform);

        if (!hasAttacked)
        {
            hasAttacked = true;
            animator.SetBool("IsAttack", true);
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
        animator.SetBool("IsAttack", false);
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            animator.SetBool("IsHit", true);
            health -= damage;
            Debug.Log("hit");
        }

        if (health <= 0)
        {
            isDead = true;
            sightRange = 0;
            attackRange = 0;
            animator.SetBool("IsDead", true);
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    public void DestroyEnemy()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        stats.AddExperience(experience);

        Destroy(gameObject);
    }
}
