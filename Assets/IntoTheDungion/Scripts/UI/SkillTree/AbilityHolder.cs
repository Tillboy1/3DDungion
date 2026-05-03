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
        bool FoundTarget = false;
        // Checks if the ability isalready contained by the player and shows the level of the ability of the ability
        for (int i = 0; i < skilltree.player.Abilities.Count; i++)
        {
            if (skilltree.player.Abilities[i].name == Ability.Name)
            {
                CurrentLvl = skilltree.player.Abilities[i].CurrentLevel;
                MaxLvl = skilltree.player.Abilities[i].MaxLevel;

                FoundTarget = true;
                Debug.Log("Found ability");
            }
        }
        if(FoundTarget == false)
        {
            CurrentLvl = Ability.CurrentLevel;
            MaxLvl = Ability.MaxLevel;
            Debug.Log("Giving ability");
        }

        skilltree.DataTitleText.text = Ability.name;


        if (CurrentLvl >= MaxLvl)
        {
            skilltree.DataCurrentSkillText.text = "Max";
            skilltree.DataButton.gameObject.SetActive(false);

            Debug.Log("maxed Out");
        }
        else if (CurrentLvl < MaxLvl && CurrentLvl >= 0 || FoundTarget)
        {
            skilltree.DataCurrentSkillText.text = "Level " + CurrentLvl.ToString();
            skilltree.DataButton.transform.GetChild(0).GetComponent<Text>().text = "Level Up";
            skilltree.DataButton.gameObject.SetActive(true);

            Debug.Log("middle of the road");
        }
        else if (FoundTarget == false)
        {
            skilltree.DataCurrentSkillText.text = "Locked";
            skilltree.DataButton.transform.GetChild(0).GetComponent<Text>().text = "Unlock";
            skilltree.DataButton.gameObject.SetActive(true);

            Debug.Log("not unlocked");
        }


        skilltree.DataDescriptionText.text = Ability.Description;
        skilltree.DataRequirments.text = Ability.ClassRequired;

        // Cnages the text if is just a ability or a entire class
        if (isForClass)
        {
            Debug.Log("checking ClassSkills");
            skilltree.DataSkillCost.text = skilltree.CurrentClasspoints + "/" + Cost.ToString();
        }
        else
        {
            Debug.Log("checking Skillpoints");
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
