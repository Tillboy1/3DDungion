using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public GameObject player;

    private string nameholder;

    private int ClassSelected;
    private int SubClassSelected;
    public TMP_Dropdown SubDropdown;

    public AbilitiesBase[] StartingAbilities;
    public WeaponBase[] StartingWeapons;
    public ArmourBase[] StartingArmour;

    public void CharacterName(string name)
    {
        nameholder = name;
    }
    public void ClassChoice(int Class)
    {
        switch (Class)
        {
            case 0:
                ClassSelected = 0;
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Sheild Focused"));
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Health Focused"));
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Damage Focused"));
                break; 
            case 1:
                ClassSelected = 1;
                SubDropdown.options.Clear();
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Melee Focused"));
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Ranged Focused"));
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Arcane Focused"));
                break;
            case 2:
                ClassSelected = 2;
                SubDropdown.options.Clear();
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("AOE Focused"));
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("Targeting Focused"));
                SubDropdown.options.Add(new TMP_Dropdown.OptionData("DamageBoost Focused"));
                break;
        }
    }
    public void SubClassChoice(int SubClass)
    {
        switch (SubClass)
        {
            case 0:
                SubClassSelected = 0;
                break;
            case 1:
                SubClassSelected = 1;
                break;
            case 2:
                SubClassSelected = 2;
                break;
        }
    }
    public void CompleateAccount()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        if (nameholder != null)
        {
            stats.characterName = nameholder;
        }
        

        //Class Selection
        if (ClassSelected <= 0)
        {
            if (SubClassSelected != 1)
            {
                stats.maxHealth.Value = 400;
            }
            else
            {
                stats.maxHealth.Value = 600;
            }
            stats.CurrentHealth.Value = stats.maxHealth.Value;

            stats.primaryAttack = true;
            stats.primaryHeals = false;
            stats.primaryDamage = 5;

            CheckStartingAbilities("Tank");
            stats.CurrentWeapon = StartingWeapons[0];

            stats.CurrentHelmet = (HelmetBase)StartingArmour[0];
            stats.CurrentChestplate = (ChestplateBase)StartingArmour[1];
            stats.CurrentLegs = (LegsBase)StartingArmour[2];
            stats.CurrentFeet = (FeetBase)StartingArmour[3];
        }
        else if (ClassSelected == 1)
        {
            stats.maxHealth.Value = 200;
            stats.CurrentHealth.Value = stats.maxHealth.Value;

            stats.primaryAttack = false;
            stats.primaryHeals = true;
            stats.primaryDamage = 5;

            CheckStartingAbilities("DPS");
            stats.CurrentWeapon = StartingWeapons[1];

            stats.CurrentHelmet = (HelmetBase)StartingArmour[4];
            stats.CurrentChestplate = (ChestplateBase)StartingArmour[5];
            stats.CurrentLegs = (LegsBase)StartingArmour[6];
            stats.CurrentFeet = (FeetBase)StartingArmour[7];
        }
        else if (ClassSelected == 2)
        {
            stats.maxHealth.Value = 150;
            stats.CurrentHealth.Value = stats.maxHealth.Value;

            stats.primaryAttack = true;
            stats.primaryHeals = true;
            stats.primaryDamage = 10;

            CheckStartingAbilities("Support");
            stats.CurrentWeapon = StartingWeapons[2];

            stats.CurrentHelmet = (HelmetBase)StartingArmour[8];
            stats.CurrentChestplate = (ChestplateBase)StartingArmour[9];
            stats.CurrentLegs = (LegsBase)StartingArmour[10];
            stats.CurrentFeet = (FeetBase)StartingArmour[11];
        }

        stats.CheckEquipment();
    }

    private void CheckStartingAbilities(string classname)
    {

        for (int i = 0; i < StartingAbilities.Length; i++)
        {
            if (StartingAbilities[i].ClassRequired == classname)
            {
                player.GetComponent<PlayerStats>().Abilities.Add(StartingAbilities[i]);
            }
        }
    }
}
