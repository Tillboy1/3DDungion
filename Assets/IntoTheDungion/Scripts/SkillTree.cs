using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public PlayerStats player;

    GameObject skillTreeGO;
    public bool SkilltreeOn;

    [Header("Universal")]
    public int ablititySight;
    public int skillpointcounts, CurrentClasspoints;

    [Header("Data Tables")]
    public bool DataSCOn;
    public GameObject DataScreen;
    public Text DataTitleText, DataCurrentSkillText, DataDescriptionText, DataRequirments, DataSkillCost;
    public Button DataButton;

    [Header("Scroll")]
    public float Scaleamount;
    public float ScaleamountMin, ScaleamountMax;
    public float Scrollamount;
    public Transform HollowClass, HollowProff;
    private GameObject[] AbilitiesBoxes;

    public void Awake()
    {
        AbilitiesBoxes = GameObject.FindGameObjectsWithTag("Data Stores");

        foreach (GameObject Go in AbilitiesBoxes)
        {
            //making it so it will altomate getting the gameobjects would be nice.... if only.
        }
    }

    public void Start()
    {
        skillTreeGO = this.gameObject;

        DataScreen = this.transform.Find("Info Screen").gameObject;
        HollowClass = this.transform.Find("Base").transform.GetChild(0).GetComponent<Transform>();
        HollowProff = this.transform.Find("Base Profession").transform.GetChild(0).GetComponent<Transform>();

        DataTitleText = DataScreen.transform.Find("Title").GetComponent<Text>();
        DataCurrentSkillText = DataScreen.transform.Find("Skill Level").GetComponent<Text>();
        DataDescriptionText = DataScreen.transform.Find("Description").GetComponent<Text>();
        DataRequirments = DataScreen.transform.Find("Reqirments").GetComponent<Text>();
        DataSkillCost = DataScreen.transform.Find("Skill Costs").GetComponent<Text>();

        DataButton = DataScreen.transform.Find("Button").GetComponent<Button>();

        //Getting data from other stats

        CurrentClasspoints = player.ClassStatsRemaining;
        skillpointcounts = player.ModifierStatsRemaining;

        //making sure it's invisable at the start
        skillTreeGO.SetActive(true);
        DataScreen.SetActive(false);

        SkilltreeOn = true;
    }
    public void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            ZoomInOut((Input.GetAxisRaw("Mouse ScrollWheel")));
        }

        if (0 < Scrollamount)
        {
            Scrollamount = Mathf.Round(Scrollamount * Mathf.Pow(10, 3)) / Mathf.Pow(10, Scrollamount);

            Scrollamount -= 0.01f;
            Scaleamount += 0.01f;

            HollowClass.transform.localScale = new Vector3(Scaleamount, Scaleamount, 1);
            /*
            foreach (GameObject Go in AbilitiesBoxes)
            {
                Go.transform.localScale = new Vector3(-Scaleamount, -Scaleamount, 1);
            }
            */
        }
        else if (0 > Scrollamount)
        {

            Scrollamount += 0.01f;

            Scaleamount -= 0.01f;

            HollowClass.transform.localScale = new Vector3(Scaleamount, Scaleamount, 1);
            /*
            foreach (GameObject Go in AbilitiesBoxes)
            {
                Go.transform.localScale = new Vector3(-Scaleamount, -Scaleamount, 1);
            }
            */
        }

        CurrentClasspoints = player.ClassStatsRemaining;
        skillpointcounts = player.ModifierStatsRemaining;

        if (!DataSCOn && Input.GetKeyDown(KeyCode.Escape))
        {
            skillTreeGO.SetActive(false);
            SkilltreeOn = false;
        }
        else if (DataSCOn && Input.GetKeyDown(KeyCode.Escape))
        {
            DataScreenClose();
        }
    }
    /*
    public enum Type
    {
        Universal,
        MageClass,
        SorcererClass,
        DruidClass,
        ClericClass,
        BardClass,
        WarlockClass,
        RougeClass,
        BloodhunterClass,
        FighterClass,
        MonkClass,
        BarbarianClass,
        RangerClass,
        ElementalClass,
        AllyClass
    }
    */
    public void ZoomInOut(float Zooming)
    {
        if (Scaleamount + Zooming > ScaleamountMin && Scaleamount + Zooming < ScaleamountMax) //This just makes it so you can't contantly zoom in or out
        {
            Scrollamount += Zooming;

            /* Scaleing theobjects
            Hollow.transform.localScale = new Vector3(Scaleamount, Scaleamount, 1);
            foreach (GameObject Go in AbilitiesBoxes)
            {
                Go.transform.localScale = new Vector3(-Scaleamount, -Scaleamount, 1);
            }
            */
        }
    }

    public void UseSkillPoints(bool classpoint, int amount)
    {
        if (classpoint)
        {
            player.ClassStatsRemaining -= amount;
        }
        else
        {
            player.ModifierStatsRemaining -= amount;
        }
    }


    #region Buttons
    void GoToButton(GameObject ButtonClicked)
    {
        HollowClass.transform.localPosition = new Vector3(-ButtonClicked.transform.localPosition.x, -ButtonClicked.transform.localPosition.y, 0);
    }

    public void OpenDataScreen(GameObject ButtonClicked)
    {
        DataSCOn = true;
        GoToButton(ButtonClicked);
        DataScreen.SetActive(true);
    }
    public void DataScreenClose()
    {
        DataSCOn = false;
        DataScreen.SetActive(false);
    }

    public void Goto0()
    {
        HollowClass.transform.localPosition = new Vector3(0, 0, 0);
        HollowProff.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void GotoSkills(Vector3 locationChosen)
    {
        HollowClass.transform.localPosition = -locationChosen;
        HollowProff.transform.localPosition = -locationChosen;
    }
    #endregion
}
