using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public enum AttackDirection
{
    Horizontal,
    Vierical,
    Diagonals,
    DiagonalUpwardsOnly,
    DiagonalDownwardsOnly,
    SurroundingArea
}
public enum AttackType
{
    Melee,                  // attack at a close range
    RangedThrown,           // range with a arc
    RangedShot,             // range with a direct shot
    SarroundingArea,        // damages nearby players
    Emmiting,               // used for calling in stuff like spikes in the ground
    summoning,              // calling in other enemies mainly used by bosses
}
public class BaseEnemy : MonoBehaviour
{
    [Header("basic Info")]
    public string EnemyName;
    [TextArea(15, 20)]
    public string description;

    public int id;
    public Sprite EnemySprite;

    //[Header("Animaton in sprite")]
    //public anim holds the animator

    [Header("health")]
    public NetworkVariable<int> currentHealth;
    public int maxHealth;
    public bool Sheild;
    public bool AbleToHeal;

    [Header("Combat Stats")]
    public int amountOfAttacks;

    public float attackSpeed;

    public AttacksSlots[] attacksPossible;

    public int XPOnDeath;
    public List<GameObject> Attackers;

    [Header("Movement")]
    public GameObject TargetGO;
    public List<GameObject> Players;
    public float distToPoint;
    public float speed;
    public bool AbleToMove;



    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatatisground, whatisplayer;
    public bool PlayerInRange;

    //patrolling
    public Vector3 walkpoint;
    protected bool walkpointset;
    public float walkPointRange;

    //Attacking
    public int baseDamage;
    public float timeBetweenAttacks;
    protected bool alreadyatacked;

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
    protected virtual void Update()
    {
        //Check for sight and attack range
        playerinsightRange = Physics.CheckSphere(transform.position, sightRange, whatisplayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisplayer);

        if (!playerinsightRange && !playerInAttackRange) Patroling();
        if (playerinsightRange && !playerInAttackRange) ChasePlayer();
        if (playerinsightRange && playerInAttackRange) AttackPlayer();
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
        float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
        float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

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
            player.GetComponent<PlayerStats>().TakeDamage(baseDamage);


            /////
            alreadyatacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    public virtual void TakeDamage(int Damage, GameObject Attacker)
    {
        if(!Attackers.Contains(Attacker))
            Attackers.Add(Attacker);
        if (currentHealth.Value - Damage <= 0)
        {
            currentHealth.Value = 0;
            Die(Attacker);
        }
        else
        {
            currentHealth.Value -= Damage;
        }
    }

    public virtual void BaseHeal(int Healing)
    {
        if (currentHealth.Value + Healing <= maxHealth)
        {
            currentHealth.Value += Healing;
        }
        else if (currentHealth.Value + Healing > maxHealth)
        {
            currentHealth.Value = maxHealth;
        }
    }

    public virtual void Die(GameObject Attacker)
    {
        for (int i = 0; i < Attackers.Count; i++)
        {
            if (Attackers[i].GetComponent<PlayerStats>())
            {
                Attackers[i].GetComponent<PlayerStats>().ReciveXP(XPOnDeath);

                if (Attackers[i].GetComponent<PlayerStats>().Targeting == this.gameObject)
                    Attackers[i].GetComponent<PlayerStats>().Targeting = null;
            }
        }

        DTime.instance.EnemiesKilled++;
        this.gameObject.SetActive(false);
    }
    public virtual void Respawn()
    {
        currentHealth.Value = maxHealth;
        this.gameObject.SetActive(true);
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
            player = other.transform;
            PlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerStats>())
        {
            player = null;
            PlayerInRange = false;
        }
    }
}
public class Attacklist
{
    public string Name;
    public string Description;
    public int Id;
    public Sprite ImageSprite;

    public int healthCurrent;
    public int healthMax;
    public bool Sheild;

    public int amountOfAttacks;

    public float attackSpeed;
    public float attackRange;

    public AttacksSlots[] AttacksPossible;

    public Attacklist(BaseEnemy enemy)
    {
        Name = enemy.EnemyName;
        Description = enemy.description;
        Id = enemy.id;
        ImageSprite = enemy.EnemySprite;

        healthCurrent = enemy.currentHealth.Value;
        healthMax = enemy.maxHealth;
        Sheild = enemy.Sheild;

        amountOfAttacks = enemy.amountOfAttacks;
        attackSpeed = enemy.attackSpeed;
        AttacksPossible = enemy.attacksPossible;

        AttacksPossible = new AttacksSlots[AttacksPossible.Length];
    }
}

[Serializable]
public class AttacksSlots
{
    public AttackDirection direction;
    public AttackType attackType;
    public GameObject prefab;

    public float AttackRange;

    public int attackDamageMin;
    public int attackDamageMax;
}