using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Collections;
using Unity.VisualScripting;


public enum AbilityState
{
    Ready,
    Active,
    Cooldown
}
[GenerateSerializationForTypeAttribute(typeof(string))]
public class PlayerStats : NetworkBehaviour
{
    private GameObject TestSpawnLocation;
    public Vector2 lastRestLocation;

    public NetworkVariable<FixedString64Bytes> characterName;

    [Header("UI")]
    public bool UIOpen;

    public GameObject UIPrefab;

    public GameObject HealthUI;
    public GameObject CharacterSheet;
    public Sprite CharacterSprite;

    [Header("Health")]
    public NetworkVariable<float> CurrentHealth;
    public NetworkVariable<float> maxHealth;
    public NetworkVariable<float> Sheild;

    //public float CurrentHealth;
    //public float maxHealth;
    //public float Sheild = 0;

    public bool interacting;
    //private bool ableToInteract = true;

    [Header("Stats")]
    public int StrengthStat;
    public int DexterityStat;
    public int ConstatutionStat;
    public int IntelligenceStat;
    public int WisdomStat;
    public int CharismaStat;

    public int ClassStatsRemaining;
    public int ModifierStatsRemaining;

    [Header("Combat")]
    public bool primaryAttack;
    public bool primaryHeals;

    public int primaryDamage;
    private bool AbleToAttack = true;

    public bool TryingToLock;
    public GameObject Targeting;
    public List<GameObject> Attackable;

    public bool AbleToMove = true;
    public bool currentlyDead = false;
    public bool IsResting;

    [Header("Leveling")]
    public NetworkVariable<int> CurrentLevel;

    public NetworkVariable<float> CurrentXp;
    public NetworkVariable<float> RequiredXp;

    public int[] XpRequireBounus;
    public float[] XpLevelBonus;

    [Header("Abilities")]
    public bool isCasting = false;
    public List<AbilitiesBase> Abilities = new List<AbilitiesBase>();

    public AbilitiesBase[] ActiveAbilities = new AbilitiesBase[10];

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
    public override void OnNetworkSpawn()
    {
        if (IsLocalPlayer)
        {
            // spawn UI
            var Menu = Instantiate(UIPrefab);

            HealthUI = Menu.GetComponentInChildren<TeamHealthUI>().gameObject;
            CharacterSheet = Menu.transform.GetChild(2).gameObject;

            Menu.GetComponentInChildren<TeamHealthUI>().PlayerObj = this.gameObject;
            Menu.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TeamHealthUI>().PlayerObj = this.gameObject;
            CharacterSheet.GetComponent<CharacterSheet>().Player = this;
            Menu.GetComponentInChildren<CharacterCreator>().player = this.gameObject;
        }
        PlayerManager.instance.PlayerJoined(this.gameObject);
    }
    public void Start()
    {
        CurrentHealth.Value = maxHealth.Value;

        this.transform.position = new Vector3(0, 7f, 0);
    }

    private void Update()
    {
        CheckAbilities();

        if (AbleToAttack && primaryAttack && Targeting != null)
        {
            StartCoroutine(PrimaryAttack());
        }
    }

    public void LockOn(InputAction.CallbackContext context)
    {
        TryingToLock = true;
    }
    public void RepeatTarget(GameObject playerTotarget)
    {
        bool Abletosee = false;

        RaycastHit rayinfo;

        /* test ui
        Vector3 rotation = Targeting.transform.position - transform.position;

        float rotY = Mathf.Atan2(-rotation.z, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, rotY, 0);

        if (Physics.Raycast(this.transform.position + new Vector3(0, 17f, 0), new Vector3(0, rotY, 0), out rayinfo, Vector3.Distance(this.transform.position, Targeting.transform.position) + 3f))
        {
            Debug.DrawLine(this.transform.position, Targeting.transform.position);
            Debug.Log(rayinfo.collider.name);

            if (rayinfo.collider.GetComponent<PlayerStats>() || rayinfo.collider.GetComponent<BaseEnemy>())
            {
                Debug.Log("Working");
                if (rayinfo.collider.gameObject == Targeting.gameObject)
                {
                    Debug.Log("Hitting target");
                    Abletosee = true;
                }

                Debug.Log(Abletosee);
            }
        }
        */
        Abletosee = true;


        /*
        if (Physics.Raycast(this.transform.position, Targeting.transform.position, out rayinfo))
        {
            if (rayinfo.collider.GetComponent<PlayerStats>() || rayinfo.collider.GetComponent<BaseEnemy>())
            {
                if (rayinfo.collider.gameObject == Targeting.gameObject)
                {
                    Abletosee = true;
                }

                Debug.Log(Abletosee);
                Debug.DrawLine(this.transform.position, Targeting.transform.position);
            }
        }
        */

        if (Abletosee)
        {
            Debug.Log("in line of Sight");
            if (playerTotarget.transform.GetComponent<BaseEnemy>() && !primaryHeals)
            {
                playerTotarget.transform.GetComponent<BaseEnemy>().TakeDamage(primaryDamage);
            }
            else if (playerTotarget.transform.GetComponent<PlayerStats>() && primaryHeals)
            {
                playerTotarget.transform.GetComponent<PlayerStats>().BaseHeal(primaryDamage);
            }
        }
        //StartCoroutine(AttackAn());
    }

    #region Abilities
    public void CheckAbilities()
    {
        for(int i = 0;i < ActiveAbilities.Length; i++)
        {
            if (ActiveAbilities[i] != null)
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
                            ActiveAbilities[i].Activate(this.gameObject);
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
    public void ActivateAbilityFive(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[4].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[4].AbilityState = AbilityState.Active;
            ActiveAbilities[4].RemainingCasting = ActiveAbilities[4].CastingTime;
        }
    }
    public void ActivateAbilitySix(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[5].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[5].AbilityState = AbilityState.Active;
            ActiveAbilities[5].RemainingCasting = ActiveAbilities[5].CastingTime;
        }
    }
    public void ActivateAbilitySeven(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[6].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[6].AbilityState = AbilityState.Active;
            ActiveAbilities[6].RemainingCasting = ActiveAbilities[6].CastingTime;
        }
    }
    public void ActivateAbilityEight(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[7].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[7].AbilityState = AbilityState.Active;
            ActiveAbilities[7].RemainingCasting = ActiveAbilities[7].CastingTime;
        }
    }
    public void ActivateAbilitynine(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[8].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[8].AbilityState = AbilityState.Active;
            ActiveAbilities[8].RemainingCasting = ActiveAbilities[8].CastingTime;
        }
    }
    public void ActivateAbilityZero(InputAction.CallbackContext context)
    {
        if (ActiveAbilities[9].AbilityState == AbilityState.Ready && !isCasting)
        {
            Debug.Log("Ability Used");
            ActiveAbilities[9].AbilityState = AbilityState.Active;
            ActiveAbilities[9].RemainingCasting = ActiveAbilities[9].CastingTime;
        }
    }
    #endregion
    #region Menu
    public void OpenUI(InputAction.CallbackContext context)
    {
        Debug.Log("trying to open Game menu");
    }
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
    #endregion
    #region Health
    public void BaseHeal(int Healing)
    {
        if (CurrentHealth.Value + Healing <= maxHealth.Value)
        {
            CurrentHealth.Value += Healing;
        }
        else if (CurrentHealth.Value + Healing > maxHealth.Value)
        {
            CurrentHealth.Value = maxHealth.Value;
        }
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Take Damage");
        if (Sheild.Value > 0)
        {
            if (Sheild.Value - damage < 0)
            {
                Debug.Log("spillover");
                float tempint = damage - Sheild.Value;

                Sheild.Value = 0;
                Debug.Log(damage + " = " + tempint);
                damage = tempint;
            }
            else if (Sheild.Value - damage == 0)
            {
                Sheild.Value = 0;
                damage = 0;
            }
            else
            {
                Debug.Log("tempHealth Damage");

                float tempint = Sheild.Value - damage;

                Sheild.Value -= tempint;
                damage = 0;
            }
        }

        if (CurrentHealth.Value - damage > 0)
        {
            CurrentHealth.Value -= damage;
            //PlayerManager.instance.LoadMasks();
            Debug.Log("showing health is not set");
        }
        else
        {
            CurrentHealth.Value = 0;
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
    #endregion

    IEnumerator WaitReact()
    {
        yield return new WaitForSeconds(.3f);

        //ableToInteract = true;
    }
    IEnumerator PrimaryAttack()
    {
        AbleToAttack = false;
        RepeatTarget(Targeting);
        yield return new WaitForSeconds(2f);
        AbleToAttack = true;

    }
    IEnumerator DeathCo()
    {
        yield return new WaitForSeconds(2);

        currentlyDead = false;

        CurrentHealth.Value = maxHealth.Value;
        this.transform.position = lastRestLocation;

        //this.GetComponent<SpriteRenderer>().color = Color.white;
        //PlayerManager.instance.LoadMasks();
    }
}
