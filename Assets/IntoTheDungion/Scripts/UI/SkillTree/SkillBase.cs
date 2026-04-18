using UnityEngine;
using UnityEngine.UI;

public class SkillBase : MonoBehaviour
{
    public SkillTree skilltree;

    public bool Skillstart, MultipathedReq, isForClass;

    //public Sprite Icon;

    [Header("costs")]
    public int MaxLvl = 5;
    public int Cost = 1; // create a cost increase
    public int[] costincrease, increasesets; // whenever the cost changes and the levels
    protected int SeqCount, CurrentLvl;

    [Header("Unlocked after")]
    public GameObject[] SequanceUnlock;
    public GameObject[] Paths;
    public int pathTemp, Pathcount; // sees how many abilitys before you can by the ability || Sees how many paths connect into the ability like 2 or 3 required to be bought first
    protected int AbilitySightLimit;

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

    public virtual void Interact() { }

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
                // used for unlocking the later abilitys
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

                    BoughtItems();

                    Interact();
                }
            }
            else if (!MultipathedReq)
            {
                // used for unlocking the later abilitys
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

                    BoughtItems();

                    skilltree.UseSkillPoints(isForClass, Cost);

                    Interact();
                } // effect
            }
        }
    }

    public virtual void BoughtItems() { }
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
