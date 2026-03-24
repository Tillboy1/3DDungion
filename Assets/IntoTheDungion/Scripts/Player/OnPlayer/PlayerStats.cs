using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public enum AbilityState
{
    Ready,
    Active,
    Cooldown
}
public class PlayerStats : MonoBehaviour
{
    private GameObject TestSpawnLocation;
    public Vector2 lastRestLocation;

    [Header("UI")]
    public bool UIOpen;
    public GameObject CharacterSheet;

    [Header("Health")]
    public float CurrentHealth;
    public float maxHealth;
    public float tempHealth = 0;

    public bool interacting;
    private bool ableToInteract = true;

    [Header("Combat")]
    public int primaryDamage;
    private bool AbleToAttack = true;

    public List<GameObject> Attackable;

    public bool AbleToMove = true;
    public bool currentlyDead = false;
    public bool IsResting;

    [Header("Abilities")]
    public bool isCasting = false;
    public List<AbilitiesBase> Abilities = new List<AbilitiesBase>();

    public AbilitiesBase[] ActiveAbilities = new AbilitiesBase[4];

    private Vector2 m_moveAmt;
    private Rigidbody2D m_rigidbodyb;

    private void Awake()
    {
        m_rigidbodyb = GetComponent<Rigidbody2D>();

        TestSpawnLocation = GameObject.FindGameObjectWithTag("TestUsage");
        CharacterSheet = GameObject.FindGameObjectWithTag("UI").transform.GetChild(1).gameObject;

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

        this.transform.position = new Vector3(0, 1.7f, 0);
    }

    private void Update()
    {
        CheckAbilities();
    }

    public void DealDamage(InputAction.CallbackContext context)
    {
        for (int i = 0; i < Attackable.Count; i++)
        {
            if (Attackable[i].transform.GetComponent<BaseEnemy>())
            {
                if (AbleToAttack)
                {
                    Attackable[i].transform.GetComponent<BaseEnemy>().TakeDamage(primaryDamage);

                    /* Soul Focus
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
                    */

                    //Reset Attacks
                    AbleToAttack = false;
                    StartCoroutine(WaitAttack());
                }
            }
            else if(1 > 0)
            {

            }
        }

        //StartCoroutine(AttackAn());
    }

    #region Abilities
    public void CheckAbilities()
    {
        for(int i = 0;i < ActiveAbilities.Length; i++)
        {
            switch (ActiveAbilities[i].AbilityState)
            {
                case AbilityState.Ready:
                    // Done with input actions
                    break;
                case AbilityState.Active:
                    isCasting = true;
                    if (ActiveAbilities[i].RemainingCasting > 0)
                    {
                        ActiveAbilities[i].RemainingCasting -= Time.deltaTime;
                    }
                    else
                    {
                        ActiveAbilities[0].Activate();
                        isCasting = false;
                        ActiveAbilities[i].AbilityState = AbilityState.Cooldown;
                    }
                    break;
                case AbilityState.Cooldown:
                    if (ActiveAbilities[i].RemainingRefresh > 0)
                    {
                        ActiveAbilities[i].RemainingRefresh -= Time.deltaTime;
                    }
                    else
                    {
                        ActiveAbilities[i].AbilityState = AbilityState.Ready;
                    }
                    break;
            }
        }
    }

    public void ActivateAbilityOne(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[0].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");

            ActiveAbilities[0].AbilityState = AbilityState.Active;
            ActiveAbilities[0].RemainingCasting = ActiveAbilities[0].CastingTime;
        }
    }
    public void ActivateAbilityTwo(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[1].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[1].AbilityState = AbilityState.Active;
            ActiveAbilities[1].RemainingCasting = ActiveAbilities[1].CastingTime;
        }
    }
    public void ActivateAbilityThree(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[2].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[2].AbilityState = AbilityState.Active;
            ActiveAbilities[2].RemainingCasting = ActiveAbilities[2].CastingTime;
        }
    }
    public void ActivateAbilityFour(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[3].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[3].AbilityState = AbilityState.Active;
            ActiveAbilities[3].RemainingCasting = ActiveAbilities[3].CastingTime;
        }
    }
    #endregion

    public void OpenCharacterSheet(InputAction.CallbackContext context)
    {
        if (CharacterSheet.activeSelf)
        {
            CharacterSheet.SetActive(false);
        }
        else
        {
            CharacterSheet.SetActive(true);
        }
    }

    public void BaseHeal(int Healing)
    {
        if (CurrentHealth + Healing <= maxHealth)
        {
            CurrentHealth += Healing;
        }
        else if (CurrentHealth + Healing > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
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

        //this.GetComponent<SpriteRenderer>().color = Color.white;
        //PlayerManager.instance.LoadMasks();
    }
}
