using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
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
        FindFirstObjectByType<TeamHealthUI>().PlayerCheck();
    }
}
