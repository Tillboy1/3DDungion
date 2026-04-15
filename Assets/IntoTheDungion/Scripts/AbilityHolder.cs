using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public SkillTree skilltree;
    [Header("basic info")]
    public string Title;
    public string Description, Requirments;
    public bool Skillstart, MultipathedReq, isForClass;

    //public Sprite Icon;

    [Header("Where Saved")]
    public string AbilityType;
    public int typecount;

    [Header("costs")]
    public int MaxLvl;
    public int Cost; // create a cost increase
    public int[] costincrease, increasesets;
    private int SeqCount, CurrentLvl;

    [Header("Unlocked after")]
    public GameObject[] SequanceUnlock;
    public GameObject[] Paths;
    private int pathTemp, Pathcount;
    private int AbilitySightLimit;

    public void Start()
    {
        //this.GetComponent<Image>().sprite = Icon;

        skilltree = FindFirstObjectByType<SkillTree>();

        AbilitySightLimit = skilltree.ablititySight;

        foreach (GameObject SqU in SequanceUnlock)
        {
            SqU.gameObject.GetComponent<AbilityHolder>().Pathcount++;
        }
    }

    public void Update()
    {

        if (Skillstart)
        {
            Sequance();
            Skillstart = false;
        }
        ColourSet();
    }

    public void Interact()
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

    public void Sequance()
    {
        foreach (GameObject SqU in SequanceUnlock)
        {
            if (SeqCount < AbilitySightLimit)
            {
                SqU.gameObject.GetComponent<AbilityHolder>().SeqCount = SeqCount + 1;
                SqU.gameObject.GetComponent<AbilityHolder>().Sequance();
                SqU.gameObject.SetActive(true);

            }
            else
            {

                SqU.gameObject.GetComponent<AbilityHolder>().SeqCount = SeqCount + 1;
                SqU.gameObject.GetComponent<AbilityHolder>().Sequance();
                SqU.gameObject.SetActive(false);

            }

            foreach (GameObject Bars in Paths)
            {
                if (SeqCount < AbilitySightLimit)
                {
                    Bars.gameObject.SetActive(true);

                    if (SeqCount == 0)
                    {
                        Bars.gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 1);
                    }
                    else if (SeqCount > 0 && SeqCount < AbilitySightLimit - 1)
                    {
                        Bars.gameObject.GetComponent<Image>().color = new Color(0, 1, 0, 1);
                    }
                    else
                    {
                        Bars.gameObject.GetComponent<Image>().color = new Color(0, 0, 1, 1);
                    }
                }
                else
                {
                    Bars.gameObject.SetActive(false);
                }
            }
        }
    }
    public void SpendPoint()
    {
        if (skilltree.skillpointcounts >= Cost && !isForClass || skilltree.CurrentClasspoints >= Cost && isForClass)
        {
            if (MultipathedReq && pathTemp == Pathcount)
            {
                if (CurrentLvl <= 0 && SeqCount <= 0)
                {
                    foreach (GameObject SqU in SequanceUnlock)
                    {
                        if (SeqCount < AbilitySightLimit)
                        {
                            SqU.gameObject.GetComponent<AbilityHolder>().SeqCount = SeqCount;
                            SqU.gameObject.GetComponent<AbilityHolder>().Sequance();
                            SqU.gameObject.SetActive(true);
                        }
                        else
                        {
                            SqU.gameObject.GetComponent<AbilityHolder>().SeqCount = SeqCount;
                            SqU.gameObject.GetComponent<AbilityHolder>().Sequance();
                            SqU.gameObject.SetActive(false);
                        }
                    }
                }
                if (SeqCount <= 0 && CurrentLvl < MaxLvl)
                {
                    CurrentLvl++;

                    for (int i = 0; i < increasesets.Length; i++)
                    {
                        if (CurrentLvl == increasesets[i])
                        {
                            Cost += costincrease[i];
                        }
                    }

                    switch (AbilityType)
                    {
                        case "Universal":
                            //CharacterSheet.Instance.UniversalSkills[typecount]++;
                            break;
                        case "MageClass":
                            //blank;
                            break;
                        case "SorcererClass":
                            //blank;
                            break;
                        case "DruidClass":
                            //blank;
                            break;
                        case "ClericClass":
                            //blank;
                            break;
                        case "BardClass":
                            //blank;
                            break;
                        case "WarlockClass":
                            //blank;
                            break;
                        case "RougeClass":
                            //blank;
                            break;
                        case "BloodhunterClass":
                            //blank;
                            break;
                        case "FighterClass":
                            //blank;
                            break;
                        case "MonkClass":
                            //blank;
                            break;
                        case "BarbarianClass":
                            //blank;
                            break;
                        case "RangerClass":
                            //blank;
                            break;
                        case "ElementalClass":
                            //blank;
                            break;
                        case "AllyClass":
                            //blank;
                            break;
                    }

                    Interact();
                }
            }
            else if (!MultipathedReq)
            {
                if (CurrentLvl <= 0 && SeqCount <= 0)
                {
                    foreach (GameObject SqU in SequanceUnlock)
                    {
                        if (SeqCount < AbilitySightLimit)
                        {
                            SqU.gameObject.SetActive(true);
                        }
                        else
                        {
                            SqU.gameObject.SetActive(false);
                        }


                        SqU.gameObject.GetComponent<AbilityHolder>().SeqCount = SeqCount;
                        SqU.gameObject.GetComponent<AbilityHolder>().Sequance();
                        SqU.gameObject.GetComponent<AbilityHolder>().pathTemp++;
                    }
                }
                if (SeqCount <= 0 && CurrentLvl < MaxLvl)
                {
                    CurrentLvl++;

                    for (int i = 0; i < increasesets.Length; i++)
                    {
                        if (CurrentLvl == increasesets[i])
                        {
                            Cost += costincrease[i];
                        }
                    }

                    switch (AbilityType)
                    {
                        case "Universal":
                            //CharacterSheet.Instance.UniversalSkills[typecount]++;
                            break;
                        case "MageClass":
                            //blank;
                            break;
                        case "SorcererClass":
                            //blank;
                            break;
                        case "DruidClass":
                            //blank;
                            break;
                        case "ClericClass":
                            //blank;
                            break;
                        case "BardClass":
                            //blank;
                            break;
                        case "WarlockClass":
                            //blank;
                            break;
                        case "RougeClass":
                            //blank;
                            break;
                        case "BloodhunterClass":
                            //blank;
                            break;
                        case "FighterClass":
                            //blank;
                            break;
                        case "MonkClass":
                            //blank;
                            break;
                        case "BarbarianClass":
                            //blank;
                            break;
                        case "RangerClass":
                            //blank;
                            break;
                        case "ElementalClass":
                            //blank;
                            break;
                        case "AllyClass":
                            //blank;
                            break;
                    }

                    skilltree.UseSkillPoints(isForClass, Cost);

                    Interact();
                } // effect
            }
        }
    }
    public void ColourSet()
    {
        if (CurrentLvl == MaxLvl)
        {
            this.GetComponent<Image>().color = new Color(0.8313726f, 0.6862745f, 0.2156863f, 1); // change to maxed out
        }
        else if (MultipathedReq && pathTemp < Pathcount)
        {
            this.GetComponent<Image>().color = new Color(0.4811321f, 0.3338976f, 0.2201406f, 1); // change to require more
        }
        else if (CurrentLvl == 0 && SeqCount == 0)
        {
            this.GetComponent<Image>().color = new Color(0.6981132f, 0.6981132f, 0.6981132f, 1); //change to possible to unlock
        }
        else if (CurrentLvl > 0)
        {
            this.GetComponent<Image>().color = new Color(1, 1, 1, 1); //change to leveled up and unlocked
        }
        else
        {
            this.GetComponent<Image>().color = new Color(0.6352941f, 0.6313726f, 0.6313726f, 0.7058824f); //change to locked
        }
    }
}
