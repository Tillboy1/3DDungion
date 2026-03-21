using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{

    private GameObject TestSpawnLocation;
    public Vector2 lastRestLocation;

    [Header("UI")]
    public bool UIOpen;

    [Header("Health")]
    public float CurrentHealth;
    public float maxHealth;
    public float tempHealth = 0;

    public bool interacting;
    private bool ableToInteract = true;

    [Header("Combat")]
    public int damage;
    private bool AbleToAttack = true;

    public bool AbleToMove = true;
    public bool currentlyDead = false;
    public bool IsResting;

    [Header("Abilities")]
    //public float focusAmount;
    //public float MaxFocus = 100;
    //public int focusOnHit = 10;
    //
    //public float FocusTaken = 70;
    //public int HealingAmount;
    //public bool AbilityReady = false;

    private Vector2 m_moveAmt;
    private Rigidbody2D m_rigidbodyb;

    private void Awake()
    {
        m_rigidbodyb = GetComponent<Rigidbody2D>();

        TestSpawnLocation = GameObject.FindGameObjectWithTag("TestUsage");
        if (TestSpawnLocation != null)
        {
            this.transform.position = TestSpawnLocation.transform.position;
        }
        else
        {
            this.transform.position = lastRestLocation;
        }
    }
    public void Start()
    {
        CurrentHealth = maxHealth;
    }
    public void Direction(InputAction.CallbackContext context)
    {
        Debug.Log("Turn PlayerCharacter");
    }

    public void DealDamage(InputAction.CallbackContext context)
    {
        /*
        List<GameObject> attackAreaObject = this.GetComponentInChildren<AttackArea>().Attackobject;

        for (int i = 0; i < attackAreaObject.Count; i++)
        {
            if (attackAreaObject[i].transform.GetComponent<Enemies>())
            {
                if (AbleToAttack)
                {
                    attackAreaObject[i].transform.GetComponent<Enemies>().TakeDamage(damage);

                    // Soul Focus
                    if (focusAmount + focusOnHit >= MaxFocus)
                    {
                        focusAmount = MaxFocus;
                    }
                    else
                    {
                        focusAmount += focusOnHit;
                    }

                    if (focusAmount >= FocusTaken)
                    {
                        AbilityReady = true;
                    }
                    PlayerManager.instance.LoadFocus();

                    //Reset Attacks
                    AbleToAttack = false;
                    StartCoroutine(WaitAttack());
                }
            }
            else if(1 > 0)
            {

            }
        }

        StartCoroutine(AttackAn());
        */
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage");
        if (tempHealth > 0)
        {
            if (tempHealth - damage < 0)
            {
                Debug.Log("spillover");
                float tempint = damage - tempHealth;

                tempHealth = 0;
                Debug.Log(damage + " = " + tempint);
                damage = tempint;
            }
            else if (tempHealth - damage == 0)
            {
                tempHealth = 0;
                damage = 0;
            }
            else
            {
                Debug.Log("tempHealth Damage");

                float tempint = tempHealth - damage;

                tempHealth -= tempint;
                damage = 0;
            }
        }

        if (CurrentHealth - damage > 0)
        {
            CurrentHealth -= damage;
            //PlayerManager.instance.LoadMasks();
            Debug.Log("showing health is not set");
        }
        else
        {
            CurrentHealth = 0;
            //PlayerManager.instance.LoadMasks();
            Die();
        }
    }
    private void Die()
    {
        currentlyDead = true;
        this.GetComponent<SpriteRenderer>().color = Color.black;
        Debug.Log("Death Animation");

        StartCoroutine(DeathCo());
    }

    IEnumerator WaitReact()
    {
        yield return new WaitForSeconds(.3f);

        ableToInteract = true;
    }
    IEnumerator WaitAttack()
    {
        yield return new WaitForSeconds(.3f);

        AbleToAttack = true;
    }
    IEnumerator DeathCo()
    {
        yield return new WaitForSeconds(2);

        currentlyDead = false;

        CurrentHealth = maxHealth;
        this.transform.position = lastRestLocation;

        this.GetComponent<SpriteRenderer>().color = Color.white;
        //PlayerManager.instance.LoadMasks();
    }
    IEnumerator AttackAn()
    {
        //attackArea.AttackShow.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        //attackArea.AttackShow.SetActive(false);
    }
}
