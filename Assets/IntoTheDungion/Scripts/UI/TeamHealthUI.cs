using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class TeamHealthUI : MonoBehaviour
{
    [Header("Current Player")]
    public GameObject PlayerObj;
    public GameObject PrimaryPlayerUI;

    [Header("Team Stats")]
    public List<GameObject> TeamObj = new List<GameObject>();
    public GameObject[] TeamUI = new GameObject[4];
    void Start()
    {
        PlayerCheck();

        int Tempcount = 0;

        foreach (Transform Childs in this.transform)
        {
            if (Childs.gameObject.name == "Current Player")
            {
                PrimaryPlayerUI = Childs.gameObject;
            }
            else
            {
                TeamUI[Tempcount-1] = Childs.gameObject;
            }
            Tempcount++;
        }
    }

    private void Update()
    {
        UpdateStats();
        UpdateTeamStats();
    }

    public void PlayerCheck()
    {
        if (TeamObj != new List<GameObject>())
        {
            //TeamObj.Clear();
        }

        Debug.Log(PlayerManager.instance.Players.Count + "count " + "shti");

        List<GameObject> tempGO = new List<GameObject>();
        for (int i = 0; i < PlayerManager.instance.Players.Count; i++)
        {
            Debug.Log(PlayerManager.instance.Players[i].gameObject.name);
            
            if (PlayerManager.instance.Players[i] != PlayerObj)
            {
                Debug.Log("Addingobj");
                tempGO.Add(PlayerManager.instance.Players[i]);
            }
            else
            {
                Debug.Log("PlayerObj");
            }
        }

        for (int i = 0; i < tempGO.Count; i++)
        {
            Debug.Log("test " + i);
            TeamObj.Add(tempGO[i]);
        }
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
            CharacterHealth.value = PlayerObj.GetComponent<PlayerStats>().CurrentHealth.Value / PlayerObj.GetComponent<PlayerStats>().maxHealth.Value;
            CharacterXP.value = PlayerObj.GetComponent<PlayerStats>().CurrentXp.Value / PlayerObj.GetComponent<PlayerStats>().RequiredXp.Value;
            //CharName.text = PlayerObj.GetComponent<PlayerStats>().characterName.Value.ToString();
            CharLevel.text = PlayerObj.GetComponent<PlayerStats>().CurrentLevel.Value.ToString();
        }
    }
    public void UpdateTeamStats()
    {

        Image PlayerSprite = null;

        Slider CharacterHealth = null;
        Slider CharacterXP = null;

        TMP_Text CharName = null;
        TMP_Text CharLevel = null;

        if (PlayerManager.instance.Players.Count >= 1)
        {
            for (int i = 0; i < TeamUI.Length; i++)
            {
                TeamUI[i].SetActive(false);
            }

            for (int i = 0; i < PlayerManager.instance.Players.Count; i++)
            {
                //Removes the main player
                if (PlayerManager.instance.Players[i] != PlayerObj)
                {
                    TeamUI[i].SetActive(true);

                    foreach (Transform Childs in TeamUI[i].transform)
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
                            Debug.Log("well this was a good idea " + Childs.gameObject);
                            CharLevel = Childs.gameObject.GetComponent<TMP_Text>();
                        }
                    }

                    //PlayerSprite.sprite = TeamObj[i].GetComponent<PlayerStats>().CharacterSprite;

                    Debug.Log(TeamObj[i].name);

                    Debug.Log(TeamObj[i].GetComponent<PlayerStats>().CurrentHealth.Value);
                    Debug.Log(TeamObj[i].GetComponent<PlayerStats>().maxHealth.Value);

                    CharacterHealth.value = TeamObj[i].GetComponent<PlayerStats>().CurrentHealth.Value / TeamObj[i].GetComponent<PlayerStats>().maxHealth.Value;
                    CharacterXP.value = TeamObj[i].GetComponent<PlayerStats>().CurrentXp.Value / TeamObj[i].GetComponent<PlayerStats>().RequiredXp.Value;
                    //CharName.text = TeamObj[i].GetComponent<PlayerStats>().characterName.Value.ToString();

                    CharLevel.text = TeamObj[i].GetComponent<PlayerStats>().CurrentLevel.Value.ToString();
                }
            }
        }
    }
}