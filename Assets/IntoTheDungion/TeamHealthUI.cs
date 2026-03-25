using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TeamHealthUI : MonoBehaviour
{
    [Header("Current Player")]
    public GameObject PlayerObj;
    public GameObject PrimaryPlayerUI;

    [Header("Team Stats")]
    public GameObject[] TeamObj;
    public GameObject[] TeamUI = new GameObject[4];
    void Start()
    {
        GameObject[] tempGO = null;
        for (int i = 0; i < PlayerManager.instance.Players.Length; i++)
        {
            if (PlayerManager.instance.Players[i] != PlayerObj)
            {
                tempGO = PlayerManager.instance.Players;
            }
        }
        TeamObj = tempGO;

        foreach (Transform Childs in this.transform)
        {
            if (Childs.gameObject.name == "Current Player")
            {
                PrimaryPlayerUI = Childs.gameObject;
            }
            else
            {
                for (int i = 0; i < TeamUI.Length; i++)
                {
                    if (TeamUI[i] = Childs.gameObject)
                    {
                        TeamUI[0] = Childs.gameObject;
                        return;
                    }
                }
            }
        }
    }

    private void Update()
    {
        UpdateStats();
        UpdateTeamStats();
    }

    public void UpdateStats()
    {
        Image PlayerSprite = null;

        Slider CharacterHealth = null;
        Slider CharacterXP = null;

        TMP_Text CharName = null;
        TMP_Text CharLevel = null;

        foreach (Transform Childs in PrimaryPlayerUI.transform)
        {
            if (Childs.GetComponent<Image>())
            {
                PlayerSprite = Childs.gameObject.GetComponent<Image>();
            }
            else if (Childs.gameObject.name == "Health Slider")
            {
                CharacterHealth = Childs.gameObject.GetComponent<Slider>();
            }
            else if (Childs.gameObject.name == "XP Slider")
            {
                CharacterXP = Childs.gameObject.GetComponent<Slider>();
            }
            else if (Childs.gameObject.name == "Chacter Name Text")
            {
                CharName = Childs.gameObject.GetComponent<TMP_Text>();
            }
            else if (Childs.gameObject.name == "Level Text")
            {
                CharLevel = Childs.gameObject.GetComponent<TMP_Text>();
            }
        }

        if (PlayerObj != null)
        {
            PlayerSprite.sprite = PlayerObj.GetComponent<PlayerStats>().CharacterSprite;
            CharacterHealth.value = PlayerObj.GetComponent<PlayerStats>().CurrentHealth / PlayerObj.GetComponent<PlayerStats>().maxHealth;
            CharacterXP.value = PlayerObj.GetComponent<PlayerStats>().CurrentXp / PlayerObj.GetComponent<PlayerStats>().RequiredXp;
            CharName.text = PlayerObj.GetComponent<PlayerStats>().CharacterName;
            CharLevel.text = PlayerObj.GetComponent<PlayerStats>().CurrentLevel.ToString();
        }
    }
    public void UpdateTeamStats()
    {

        Sprite PlayerSprite;

        Slider CharacterHealth = null;
        Slider CharacterXP = null;

        TMP_Text CharName = null;
        TMP_Text CharLevel = null;

        for (int i = 0; i < TeamUI.Length; i++)
        {
            if (PlayerManager.instance.Players.Length <= i)
            {
                TeamUI[i].SetActive(true);

                foreach (Transform Childs in PrimaryPlayerUI.transform)
                {
                    if (Childs.GetComponent<Image>())
                    {
                        PlayerSprite = Childs.gameObject.GetComponent<Image>().sprite;
                    }
                    else if (Childs.gameObject.name == "Health Slider")
                    {
                        CharacterHealth = Childs.gameObject.GetComponent<Slider>();
                    }
                    else if (Childs.gameObject.name == "XP Slider")
                    {
                        CharacterXP = Childs.gameObject.GetComponent<Slider>();
                    }
                    else if (Childs.gameObject.name == "Chacter Name Text")
                    {
                        CharName = Childs.gameObject.GetComponent<TMP_Text>();
                    }
                    else if (Childs.gameObject.name == "Level Text")
                    {
                        CharLevel = Childs.gameObject.GetComponent<TMP_Text>();
                    }
                }

                PlayerSprite = TeamObj[i].GetComponent<PlayerStats>().CharacterSprite;
                CharacterHealth.value = TeamObj[i].GetComponent<PlayerStats>().CurrentHealth / TeamObj[i].GetComponent<PlayerStats>().maxHealth;
                CharacterXP.value = TeamObj[i].GetComponent<PlayerStats>().CurrentXp / TeamObj[i].GetComponent<PlayerStats>().RequiredXp;
                CharName.text = TeamObj[i].GetComponent<PlayerStats>().CharacterName;
                CharLevel.text = TeamObj[i].GetComponent<PlayerStats>().CurrentLevel.ToString();
            }
            else
            {
                TeamUI[i].SetActive(false);
            }
        }
    }
}
