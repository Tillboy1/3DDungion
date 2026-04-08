using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{
    public GameObject player;

    private int ClassSelected;
    public AbilitiesBase[] StartingAbilities;
    public void ClassChoice(int Class)
    {
        switch (Class)
        {
            case 0:
                ClassSelected = 0;
                break; 
            case 1:
                ClassSelected = 1;
                break;
            case 2:
                ClassSelected = 2;
                break;
        }
    }
    public void CompleateAccount()
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (ClassSelected <= 0)
        {
            stats.maxHealth.Value = 400;
            stats.CurrentHealth.Value = stats.maxHealth.Value;
            stats.Sheild.Value = 100;

            stats.primaryAttack = true;
            stats.primaryHeals = false;
            stats.primaryDamage = 5;

            CheckStartingAbilities("Tank");
        }
        else if (ClassSelected == 1)
        {
            stats.maxHealth.Value = 200;
            stats.CurrentHealth.Value = stats.maxHealth.Value;
            stats.Sheild.Value = 25;

            stats.primaryAttack = false;
            stats.primaryHeals = true;
            stats.primaryDamage = 5;

            CheckStartingAbilities("DPS");
        }
        else if (ClassSelected == 2)
        {
            stats.maxHealth.Value = 150;
            stats.CurrentHealth.Value = stats.maxHealth.Value;
            stats.Sheild.Value = 0;

            stats.primaryAttack = true;
            stats.primaryHeals = true;
            stats.primaryDamage = 10;

            CheckStartingAbilities("Support");
        }
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
