using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class MassEnemy : BaseEnemy
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject playerStateSystem;
    public LayerMask whatatisground, whatisplayer;

    public bool PlayerInRange;

    //patrolling
    public Vector3 walkpoint;
    bool walkpointset;
    public float walkPointRange;

    //Attacking
    public int damagetogive;

    public float timeBetweenAttacks;
    bool alreadyatacked;
    public string DEATHscreen;

    //states
    public float sightRange, attackRange;
    public bool playerinsightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        currentHealth.Value = maxHealth;
    }
    private void Update()
    {
        //Check for sight and attack range
        playerinsightRange = Physics.CheckSphere(transform.position, sightRange, whatisplayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisplayer);

        if (!playerinsightRange && !playerInAttackRange) Patroling();
        if (playerinsightRange && !playerInAttackRange) ChasePlayer();
        if (playerinsightRange && playerInAttackRange) AttackPlayer();
    }
    public virtual void Respawn()
    {
        currentHealth.Value = maxHealth;
        this.gameObject.SetActive(true);
    }
    private void Patroling()
    {
        if (!walkpointset) Searchwalkpoint();

        if (walkpointset)
            agent.SetDestination(walkpoint);

        Vector3 distancetowalkpoint = transform.position - walkpoint;

        //walkpointreached
        if (distancetowalkpoint.magnitude < 1f)
            walkpointset = false;
    }
    private void Searchwalkpoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, whatatisground))
            walkpointset = true;
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    public virtual void AttackPlayer()
    {

        //make sure enemydosn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        if (!alreadyatacked)
        {
            //Attack code input here
            player.GetComponent<PlayerStats>().TakeDamage(damagetogive);


            /////
            alreadyatacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyatacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            Debug.Log("Player Entered Range");
            other.GetComponent<PlayerStats>().TakeDamage(damagetogive);
        }
        if (other.CompareTag("Player"))
        {
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInRange = false;
        }
    }
}