using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoomStats : MonoBehaviour
{
    public GameObject[] doorways; // Gets All of the rooms next to it || Only Counts exit doors
    public GameObject dungionGenerator; // Gets the manager
    public List<GameObject> AvalableRooms = new List<GameObject>();

    public float Radius;
    public int AmountOfRoomsLeft = 0;
    public bool IsWayToExit = false;

    public void LoadingDungion()
    {
        //checks which rooms are avalable with room amount limitations
        for (int i = 0; i < dungionGenerator.GetComponent<EntrenceRoom>().RoomPrefabs.Length; i++)
        {
            AvalableRooms.Clear();

            if (dungionGenerator.GetComponent<EntrenceRoom>().RoomPrefabs[i].GetComponent<RoomStats>().doorways.Length <= AmountOfRoomsLeft - 1)
            {
                AvalableRooms.Add(dungionGenerator.GetComponent<EntrenceRoom>().RoomPrefabs[i]);
            }
        }

        //Sets The Path To The Exit
        int chozenPath = Random.Range(0, doorways.Length);
        doorways[chozenPath].GetComponent<Doorways>().IsWayToExit = IsWayToExit;

        // sets the amount of doorways per path
        for (int i = 0; i < doorways.Length; i++)
        {
            //setting the amount of rooms left per path way
            int roomsset = Random.Range(0, AmountOfRoomsLeft);
            doorways[i].GetComponent<Doorways>().AmountOfRoomsLeft = roomsset;
        }

        // Raycast from the doors to see if there is any rooms in the direction of the path

            // if there is a doorway add the rooms left to the amount and if they if it is the path the the exit

            // if no door in the way make new room and all the details
    }
}
