using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class CharacterSheet : MonoBehaviour
{
    public GameObject Player;

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

    public TMP_Text AbilityNameText;
    public TMP_Text AbilityDescriptionText;
    public TMP_Text AbilityClassText;
    public TMP_Text AbilityactivateTimeText;
    public TMP_Text AbilityDurationTimeText;
    public TMP_Text AbilityRefreshTimeText;

    [Header("Equipment")]
    //                                      Equipment
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

                foreach (Transform AbilitySlots in objects.GetChild(0))
                {
                    AbilityIcons[tempcount] = AbilitySlots.GetChild(0).gameObject;
                    tempcount++;
                }
                foreach (Transform item in objects.GetChild(2))
                {
                    if (item.gameObject.name == "Name Text (TMP)")
                    {
                        AbilityNameText = objects.GetChild(2).GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Class Text (TMP)")
                    {
                        AbilityDescriptionText = objects.GetChild(2).GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Description Text (TMP)")
                    {
                        AbilityClassText = objects.GetChild(2).GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "CastTime Text (TMP)")
                    {
                        AbilityactivateTimeText = objects.GetChild(2).GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Duration Text (TMP)")
                    {
                        AbilityDurationTimeText = objects.GetChild(2).GetComponent<TMP_Text>();
                    }
                    else if (item.gameObject.name == "Recharge Text (TMP)")
                    {
                        AbilityRefreshTimeText = objects.GetChild(2).GetComponent<TMP_Text>();
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
    }
}
