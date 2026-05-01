using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : SkillBase
{
    [Header("basic info")]
    public AbilitiesBase Ability;

    //public Sprite Icon;



    public override void Interact()
    {
        // Checks if the ability isalready contained by the player and shows the level of the ability of the ability
        for (int i = 0; i < skilltree.player.Abilities.Count; i++)
        {
            if (skilltree.player.Abilities[i].name == Ability.Name)
            {
                CurrentLvl = Ability.CurrentLevel;
                MaxLvl = Ability.MaxLevel;
                return;
            }
        }

        skilltree.DataTitleText.text = Ability.name;

        if (CurrentLvl >= MaxLvl)
        {
            skilltree.DataCurrentSkillText.text = "Max";
            skilltree.DataButton.gameObject.SetActive(false);
        }
        else if (CurrentLvl < MaxLvl && CurrentLvl > 0)
        {
            skilltree.DataCurrentSkillText.text = "Level " + CurrentLvl.ToString();
            skilltree.DataButton.transform.GetChild(0).GetComponent<Text>().text = "Level Up";
            skilltree.DataButton.gameObject.SetActive(true);
        }
        else
        {
            skilltree.DataCurrentSkillText.text = "Locked";
            skilltree.DataButton.transform.GetChild(0).GetComponent<Text>().text = "Unlock";
            skilltree.DataButton.gameObject.SetActive(true);
        }


        skilltree.DataDescriptionText.text = Ability.Description;
        skilltree.DataRequirments.text = Ability.ClassRequired;

        // Cnages the text if is just a ability or a entire class
        if (isForClass)
        {
            skilltree.DataSkillCost.text = skilltree.CurrentClasspoints + "/" + Cost.ToString();
        }
        else
        {
            skilltree.DataSkillCost.text = skilltree.skillpointcounts + "/" + Cost.ToString();
        }

        skilltree.OpenDataScreen(this.gameObject);

        skilltree.DataButton.onClick.RemoveAllListeners();
        skilltree.DataButton.onClick.AddListener(this.gameObject.GetComponent<AbilityHolder>().SpendPoint);
    }

    public override void BoughtItems()
    {
        bool anyfound = false;
        for (int i = 0; i < skilltree.player.Abilities.Count; i++)
        {
            if (skilltree.player.Abilities[i].name == Ability.Name)
            {
                if (skilltree.player.Abilities[i].CurrentLevel < skilltree.player.Abilities[i].MaxLevel)
                {
                    skilltree.player.Abilities[i].CurrentLevel++;
                }
                anyfound = true;
                return;
            }
        }
        if (!anyfound)
        {
            skilltree.player.Abilities.Add(Ability);
        }
    }
}
