using UnityEngine;
using TMPro;
using System.Collections.Generic;
using Unity.VisualScripting;

public class DTime : MonoBehaviour
{
    public static DTime instance;
    public GameObject World;

    [Header("Gets the Text")]
    public List<GameObject> Players;
    public TMP_Text TimerText;

    [Header("Counters")]
    [Tooltip("counted in minutes")]
    public float TimeGiven;
    public float Remaining;
    public int Remaintextcount;

    [Header("Enemies")]
    public List<GameObject> bosses;
    public int RequiredDeaths;
    public List<GameObject> enemies;
    public int BossesKilled;
    public int EnemiesKilled;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (Transform T in World.transform)
        {
            foreach (Transform T2 in T.transform)
            {
                if (T2.GetComponent<MassEnemy>())
                {
                    enemies.Add(T2.gameObject);
                }
                else if (T2.GetComponent<MassEnemy>())
                {
                    enemies.Add(T2.gameObject);
                }
            }
        }
    }

    public void Start()
    {
        Remaining = TimeGiven * 60;

        CheckPlayers();
    }

    public void Update()
    {
        Countdown();
        CheckWin();
    }

    public void CheckPlayers()
    {
        Players = PlayerManager.instance.Players;

        /* sets stats
        for (int i = 0; i < Players.Count; i++)
        {

        }
        */
    }
    public void BossDefeated()
    {
        BossesKilled++;
    }


    public void Countdown()
    {
        if (Remaining > 0)
        {
            Remaining -= Time.deltaTime;
        }
        else if (Remaining < 0)
        {
            Remaining = 0;
        }
    }
    public void CheckWin()
    {
        int livingenemies = 0;

        if(Remaining > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].activeSelf)
                {
                    livingenemies++;
                }
            }

            if(RequiredDeaths <= livingenemies && bosses.Count <= BossesKilled)
            {
                //WinScreen
                for (int i = 0; i < Players.Count; i++)
                {
                    Players[i].GetComponent<PlayerStats>().ConclusionUI.GetComponent<ConclusionScreen>().OpenConclusion(true, Remaining);
                }
            }
        }
        else
        {
            Debug.Log("OutOfTime");

            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].GetComponent<PlayerStats>().ConclusionUI.GetComponent<ConclusionScreen>().OpenConclusion(false, Remaining);
            }
        }
    }
}
