using UnityEngine;
using UnityEngine.UI;

public class SkillsHolder : SkillBase
{

    [Header("basic info")]
    public string Title;

    [TextArea(10, 5)]
    public string Description;
    public string Requirments;

    //public Sprite Icon;


    //public int  CurrentLvl;



    public override void Interact()
    {
        skilltree.DataTitleText.text = Title;

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


        skilltree.DataDescriptionText.text = Description;
        skilltree.DataRequirments.text = Requirments;

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

    }
}
