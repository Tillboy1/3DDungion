using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CharacterSheet : MonoBehaviour
{
    public PlayerStats Player;

    public TMP_Text[] StatsText = new TMP_Text[6];

    public TMP_Text Armour;

    public TMP_Text Health;
    public TMP_Text TempHealth;

    [Header("Abilities")]
    //                                      Ability
    public GameObject AbilityScreen;

    public GameObject[] AbilityIcons = new GameObject[10];
    public GameObject AbilitySlots;
    public GameObject AbilityPrefab;

    public int SlectedAbilitySlot;
    public AbilitiesBase SlectedAbility;

    public TMP_Text AbilityNameText;
    public TMP_Text AbilityDescriptionText;
    public TMP_Text AbilityClassText;
    public TMP_Text AbilityactivateTimeText;
    public TMP_Text AbilityDurationTimeText;
    public TMP_Text AbilityRefreshTimeText;

    //                                      Equipment
    [Header("Equipment")]
    public GameObject EquipmentScreen;

    public GameObject HealmetSlot;
    public GameObject ChestSlot;
    public GameObject LegsSlot;
    public GameObject FeetSlot;

    public GameObject MeleeSlot;
    public GameObject RangedSlot;

    public GameObject HealmetArmourSection;
    public GameObject ChestArmourSection;
    public GameObject LegsArmourSection;
    public GameObject FeetArmourSection;

    public GameObject MeleeWeaponSection;
    public GameObject RangedWeaponSection;

    public TMP_Text EquipmentNameText;
    public TMP_Text EquipmentDescriptionText;
    public TMP_Text EquipmentClassText;
    public TMP_Text EquipmentactivateTimeText;
    public TMP_Text EquipmentDurationTimeText;
    public TMP_Text EquipmentRefreshTimeText;

    [Header("Skills")]
    //                                      Equipment
    public GameObject SkillsScreen;

    public void Start()
    {
        foreach(Transform objects in this.transform)
        {
            if(objects.gameObject.name == "Modifires")
            {
                int tempcount = 0;
                foreach (Transform Modifiers in objects)
                {
                    StatsText[tempcount] = Modifiers.GetChild(3).GetComponent<TMP_Text>();
                    tempcount++;
                }
            }
            else if (objects.gameObject.name == "Health")
            {
                Health = objects.GetChild(0).GetComponent<TMP_Text>();
                TempHealth = objects.GetChild(1).transform.GetChild(0).GetComponent<TMP_Text>();
            }
            else if (objects.gameObject.name == "Armour")
            {
                Armour = objects.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            }
            else if (objects.gameObject.name == "Abilitys")
            {
                int tempcount = 0;
                AbilityScreen = objects.gameObject;

                // Current Ability Slots
                foreach (Transform AbilitySlots in objects.GetChild(0))
                {
                    AbilityIcons[tempcount] = AbilitySlots.GetChild(0).gameObject;
                    tempcount++;
                }
                AbilitySlots = objects.GetChild(1).gameObject;

                //InfoTab
                foreach (Transform item in objects.GetChild(2))
                {
                    if (item.gameObject.name == "Name Text (TMP)")
                    {
                        AbilityNameText = item.GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Description Text (TMP)")
                    {
                        AbilityDescriptionText = item.GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Class Text (TMP)")
                    {
                        AbilityClassText = item.GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "CastTime Text (TMP)")
                    {
                        AbilityactivateTimeText = item.GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Duration Text (TMP)")
                    {
                        AbilityDurationTimeText = item.GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Recharge Text (TMP)")
                    {
                        AbilityRefreshTimeText = item.GetComponent<TMP_Text>();
                    }
                }
            }
            else if (objects.gameObject.name == "Equipment")
            {
                EquipmentScreen = objects.gameObject;
            }
            else if (objects.gameObject.name == "Skills")
            {
                SkillsScreen = objects.gameObject;
            }
        }
        ShowAbilities();
    }
    public void SelectedAbilitySlot(int SlotNumber)
    {
        SlectedAbilitySlot = SlotNumber;
    }
    public void UpdateAbilityInfo(AbilitiesBase AbilityTS)
    {
        AbilityNameText.text = AbilityTS.Name;
        AbilityDescriptionText.text = AbilityTS.Description;
        AbilityClassText.text = AbilityTS.ClassRequired;
        AbilityactivateTimeText.text = AbilityTS.CastingTime.ToString();
        AbilityDurationTimeText.text = AbilityTS.DurationTime.ToString();
        AbilityRefreshTimeText.text = AbilityTS.RefreshTime.ToString();

        SlectedAbility = AbilityTS;
    }
    public void ChangeAbility()
    {
        if(Player.ActiveAbilities[SlectedAbilitySlot] != null)
        {
            //switching out the abilities that are active
            Player.Abilities.Add(Player.ActiveAbilities[SlectedAbilitySlot]);
            Player.Abilities.Remove(SlectedAbility);

            Player.ActiveAbilities[SlectedAbilitySlot] = SlectedAbility;
        }
        else
        {
            Player.ActiveAbilities[SlectedAbilitySlot] = SlectedAbility;
            Player.Abilities.Remove(SlectedAbility);
        }
        ShowAbilities();
    }

    public void ShowAbilities()
    {
        // Active Abilities
        for (int i = 0; i < Player.ActiveAbilities.Length; i++)
        {
            if (Player.ActiveAbilities[i] != null)
            {
                AbilityIcons[i].GetComponent<Image>().sprite = Player.ActiveAbilities[i].sprite;
            }
        }

        // Non active abilities
        for (int i = 0; i < Player.Abilities.Count; i++)
        {
            var GO = Instantiate(AbilityPrefab, AbilitySlots.transform);
            GO.GetComponent<AbilityHolders>().HeldAbility = Player.Abilities[i];
            GO.GetComponent<AbilityHolders>().CharacterSheet = this;
        }
    }
}