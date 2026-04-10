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
    public GameObject WeaponPrefab;
    public GameObject ArmourPrefab;

    public int SlectedAbilitySlot;
    public AbilitiesBase SlectedAbility;
    public WeaponBase SlectedWeapon;
    public ArmourBase SlectedArmour;

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

    //holding items sections
    public GameObject HealmetArmourSection;
    public GameObject ChestArmourSection;
    public GameObject LegsArmourSection;
    public GameObject FeetArmourSection;

    public GameObject MeleeWeaponSection;
    public GameObject RangedWeaponSection;
    public GameObject ArcaneWeaponSection;

    public TMP_Text EquipmentNameText;
    public TMP_Text EquipmentDescriptionText;
    public TMP_Text EquipmentClassText;

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

                foreach (Transform item in objects)
                {
                    if (item.name == "Equipment Screen")
                    {
                        foreach (Transform Section in item)
                        {
                            if (Section.name == "Armour")
                            {
                                foreach(Transform ArmourPart in Section)
                                {
                                    if (ArmourPart.name == "Head Armour Image")
                                    {
                                        HealmetSlot = ArmourPart.gameObject;
                                    }
                                    if (ArmourPart.name == "Chest Armour Image")
                                    {
                                        ChestSlot = ArmourPart.gameObject;
                                    }
                                    if (ArmourPart.name == "Legs Armour Image")
                                    {
                                        LegsSlot = ArmourPart.gameObject;
                                    }
                                    if (ArmourPart.name == "Feet Armour Image")
                                    {
                                        FeetSlot = ArmourPart.gameObject;
                                    }
                                }
                            }
                            if (Section.name == "Weapons")
                            {
                                foreach( Transform WeaponPart in Section)
                                {
                                    if (WeaponPart.name == "Melee Weapon Image")
                                    {
                                        MeleeSlot = WeaponPart.gameObject;
                                    }
                                    if (WeaponPart.name == "Ranged Weapon Image")
                                    {
                                        RangedSlot = WeaponPart.gameObject;
                                    }
                                }
                            }
                            if (Section.name == "Buttons")
                            {

                            }
                            if (Section.name == "Armour Slider")
                            {
                                foreach(Transform Collection in Section.GetChild(0).transform)
                                {
                                    if (Collection.name == "Head Armour Slot")
                                    {
                                        HealmetArmourSection = Collection.gameObject;
                                    }
                                    if (Collection.name == "Chest Armour Slot")
                                    {
                                        ChestArmourSection = Collection.gameObject;
                                    }
                                    if (Collection.name == "Legs Armour Slot")
                                    {
                                        LegsArmourSection = Collection.gameObject;
                                    }
                                    if (Collection.name == "Feet Armour Slot")
                                    {
                                        FeetArmourSection = Collection.gameObject;
                                    }
                                }
                            }
                            if (Section.name == "Weapons Slider")
                            {
                                foreach (Transform Collection in Section.GetChild(0).transform)
                                {
                                    if (Collection.name == "Melee Weapon Slot")
                                    {
                                        MeleeWeaponSection = Collection.gameObject;
                                    }
                                    if (Collection.name == "Ranged Weapon Slot")
                                    {
                                        RangedWeaponSection = Collection.gameObject;
                                    }
                                    if (Collection.name == "Arcane Weapon Slot")
                                    {
                                        ArcaneWeaponSection = Collection.gameObject;
                                    }
                                }
                            }
                        }
                    }
                    else if (item.name == "Ability Info")
                    {
                        foreach (Transform Slots in item)
                        {
                            if (Slots.gameObject.name == "Name Text (TMP)")
                            {
                                EquipmentNameText = Slots.GetComponent<TMP_Text>();
                            }
                            else if (Slots.gameObject.name == "Description Text (TMP)")
                            {
                                EquipmentDescriptionText = Slots.GetComponent<TMP_Text>();
                            }
                            else if (Slots.gameObject.name == "Modifier Text (TMP)")
                            {
                                EquipmentClassText = Slots.GetComponent<TMP_Text>();
                            }
                        }
                    }
                }
            }
            else if (objects.gameObject.name == "Skills")
            {
                SkillsScreen = objects.gameObject;
            }
        }

        //Sets All the UI
        ShowAbilities();
        ShowWeapons();
        ShowArmour();
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
    public void UpdateWeaponInfo(WeaponBase WeaponTS)
    {
        EquipmentNameText.text = WeaponTS.Name;
        EquipmentDescriptionText.text = WeaponTS.Description;
        //EquipmentClassText.text = WeaponTS.

        SlectedWeapon = WeaponTS;
        SlectedArmour = null;
    }
    public void UpdateArmourInfo(ArmourBase ArmourTS)
    {
        EquipmentNameText.text = ArmourTS.Name;
        EquipmentDescriptionText.text = ArmourTS.Description;
        //EquipmentClassText.text = WeaponTS.

        SlectedWeapon = null;
        SlectedArmour = ArmourTS;
    }
    public void ChangeAbility()
    {
        if (Player.ActiveAbilities[SlectedAbilitySlot] != null)
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
    public void ChangeEquipment(int Slot)
    {
        if (SlectedWeapon != null)
        {
            if (Player.ActiveAbilities[SlectedAbilitySlot] != null)
            {
                //switching out the abilities that are active


                Player.Abilities.Remove(SlectedAbility);
                Player.ActiveAbilities[SlectedAbilitySlot] = SlectedAbility;
            }
            else
            {
                Player.Abilities.Remove(SlectedAbility);
            }

            if (Slot == 0)
            {
                Player.CurrentWeapon = SlectedWeapon;
            }
            else if (Slot == 1)
            {
                Player.BackUpWeapon = SlectedWeapon;
            }

            ShowWeapons();
        }
        else if (SlectedArmour != null)
        {
            if (SlectedArmour.GetComponent<HelmetBase>())
            {
                if (Player.CurrentHelmet != null)
                {
                    //switching out the armour that are active
                    Player.OwnedArmour.Add(Player.CurrentHelmet);
                    Player.OwnedArmour.Remove(SlectedArmour);
                }
                else
                {
                    Player.OwnedArmour.Remove(SlectedArmour);
                }

                Player.CurrentHelmet = SlectedArmour.GetComponent<HelmetBase>();
            }
            else if (SlectedArmour.GetComponent<ChestplateBase>())
            {
                if (Player.CurrentChestplate != null)
                {
                    //switching out the armour that are active
                    Player.OwnedArmour.Add(Player.CurrentChestplate);
                    Player.OwnedArmour.Remove(SlectedArmour);
                }
                else
                {
                    Player.OwnedArmour.Remove(SlectedArmour);
                }

                Player.CurrentChestplate = SlectedArmour.GetComponent<ChestplateBase>();
            }
            else if (SlectedArmour.GetComponent<LegsBase>())
            {
                if (Player.CurrentLegs != null)
                {
                    //switching out the armour that are active
                    Player.OwnedArmour.Add(Player.CurrentLegs);
                    Player.OwnedArmour.Remove(SlectedArmour);
                }
                else
                {
                    Player.OwnedArmour.Remove(SlectedArmour);
                }

                Player.CurrentLegs = SlectedArmour.GetComponent<LegsBase>();
            }
            else if (SlectedArmour.GetComponent<FeetBase>())
            {
                if (Player.CurrentFeet != null)
                {
                    //switching out the armour that are active
                    Player.OwnedArmour.Add(Player.CurrentFeet);
                    Player.OwnedArmour.Remove(SlectedArmour);
                }
                else
                {
                    Player.OwnedArmour.Remove(SlectedArmour);
                }

                Player.CurrentFeet = SlectedArmour.GetComponent<FeetBase>();
            }

            ShowArmour();
        }
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

        foreach (Transform item in AbilitySlots.transform)
        {
            Destroy(item.gameObject);
        }

        // Non active abilities
        for (int i = 0; i < Player.Abilities.Count; i++)
        {
            var GO = Instantiate(AbilityPrefab, AbilitySlots.transform);
            GO.GetComponent<AbilityHolders>().HeldAbility = Player.Abilities[i];
            GO.GetComponent<AbilityHolders>().CharacterSheet = this;
        }
    }

    public void ShowCurrentEquipment()
    {

    }
    public void ShowWeapons()
    {

        foreach (Transform item in MeleeWeaponSection.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in RangedWeaponSection.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in ArcaneWeaponSection.transform)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < Player.OwnedWeapons.Count; i++)
        {
            Debug.Log("Got to show weapons " + Player.OwnedWeapons[i].name);

            if (Player.OwnedWeapons[i] is MeleeWeapon)
            {
                var GO = Instantiate(WeaponPrefab, MeleeWeaponSection.transform);
                GO.GetComponent<WeaponHolder>().HeldWeapon = Player.OwnedWeapons[i];
                GO.GetComponent<WeaponHolder>().CharacterSheet = this;
            }
            else if (Player.OwnedWeapons[i] is RangedWeapon)
            {
                var GO = Instantiate(WeaponPrefab, RangedWeaponSection.transform);
                GO.GetComponent<WeaponHolder>().HeldWeapon = Player.OwnedWeapons[i];
                GO.GetComponent<WeaponHolder>().CharacterSheet = this;
            }
            else if (Player.OwnedWeapons[i] is CasterWeapon)
            {
                var GO = Instantiate(WeaponPrefab, ArcaneWeaponSection.transform);
                GO.GetComponent<WeaponHolder>().HeldWeapon = Player.OwnedWeapons[i];
                GO.GetComponent<WeaponHolder>().CharacterSheet = this;
            }
        }
    }
    public void ShowArmour()
    {
        //Removing all Old Armour
        foreach (Transform item in HealmetArmourSection.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in ChestArmourSection.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in LegsArmourSection.transform)
        {
            Destroy(item.gameObject);
        }
        foreach (Transform item in FeetArmourSection.transform)
        {
            Destroy(item.gameObject);
        }

        Debug.Log("going to add " + Player.OwnedArmour.Count);

        // Non active abilities
        for (int i = 0; i < Player.OwnedArmour.Count; i++)
        {
            if (Player.OwnedArmour[i] is HelmetBase)
            {
                Debug.Log("It is a Helmet");
                var GO = Instantiate(ArmourPrefab, HealmetArmourSection.transform);
                GO.GetComponent<ArmourHolder>().HeldArmour = Player.OwnedArmour[i];
                GO.GetComponent<ArmourHolder>().CharacterSheet = this;
            }
            else if (Player.OwnedArmour[i] is ChestplateBase)
            {
                var GO = Instantiate(ArmourPrefab, ChestArmourSection.transform);
                GO.GetComponent<ArmourHolder>().HeldArmour = Player.OwnedArmour[i];
                GO.GetComponent<ArmourHolder>().CharacterSheet = this;
            }
            else if (Player.OwnedArmour[i] is LegsBase)
            {
                var GO = Instantiate(ArmourPrefab, LegsArmourSection.transform);
                GO.GetComponent<ArmourHolder>().HeldArmour = Player.OwnedArmour[i];
                GO.GetComponent<ArmourHolder>().CharacterSheet = this;
            }
            else if (Player.OwnedArmour[i] is FeetBase)
            {
                var GO = Instantiate(ArmourPrefab, FeetArmourSection.transform);
                GO.GetComponent<ArmourHolder>().HeldArmour = Player.OwnedArmour[i];
                GO.GetComponent<ArmourHolder>().CharacterSheet = this;
            }
        }
    }
}