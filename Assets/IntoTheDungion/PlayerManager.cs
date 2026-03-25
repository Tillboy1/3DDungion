using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using UnityEngine.Rendering;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    public GameObject[] Players;


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
        for (int i = 0; i < Players.Length; i++)
        {
            if (Players[i] != null)
            {
                Players[i] = PlayerJoining.gameObject;
                return;
            }
            Debug.Log("To many Players"); 
        }
    }
}
