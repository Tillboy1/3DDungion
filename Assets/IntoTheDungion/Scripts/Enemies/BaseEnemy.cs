using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

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

    [Header("Movement")]
    public GameObject TargetGO;
    public List<GameObject> Players;
    public float distToPoint;
    public float speed;
    public bool AbleToMove;

    public void ClosesestPlayer()
    {
        float TempPlayerDistances = 1000000000;
        if (Players.Count > 0)
        {
            distToPoint = Vector2.Distance(transform.position, Players[0].transform.position);
            TargetGO = Players[0];
        }
        else
        {
            Debug.Log("player more");
            distToPoint = Vector2.Distance(transform.position, Players[0].transform.position);
            TargetGO = Players[0];
            for (int i = 0; i < Players.Count; i++)
            {
                TempPlayerDistances = Vector2.Distance(transform.position, Players[i].transform.position);
                if (TempPlayerDistances < distToPoint)
                {
                    distToPoint = TempPlayerDistances;
                    TargetGO = Players[i];
                }
                if (TempPlayerDistances == distToPoint)
                {
                    Debug.Log("Same");
                }
            }
        }
    }

    public virtual void TakeDamage(int Damage)
    {
        if (currentHealth.Value - Damage <= 0)
        {
            currentHealth.Value -= Damage;
            Die();
        }
        else
        {
            currentHealth.Value -= Damage;
        }
    }

    public virtual void BaseHeal(int Heal)
    {

    }

    public virtual void Die()
    {
        this.gameObject.SetActive(false);
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
