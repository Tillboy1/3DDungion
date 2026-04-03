using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PlayerManager : NetworkBehaviour
{
    public static PlayerManager instance;
    public List<GameObject> Players;

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
    }

    public void PlayerJoined(GameObject PlayerJoining)
    {
        Players.Add(PlayerJoining.gameObject);

        PlayerJoining.name = ("Player " + Players.Count.ToString());

        if (PlayerJoining.GetComponent<PlayerStats>().HealthUI)
        {
            PlayerJoining.GetComponent<PlayerStats>().HealthUI.GetComponent<TeamHealthUI>().PlayerCheck();
        }
    }
}
