using UnityEngine;

public class EntrenceRoom : MonoBehaviour
{
    public GameObject FirstDoorway;
    public GameObject[] RoomPrefabs;
    public int RoomsMin, RoomsMax;
    private int RoomAmount;

    public void Start()
    {
        RoomAmount = Random.Range(RoomsMin, RoomsMax);

        //SummonDungion();
    }
    public void SummonDungion()
    {
        int StartRoom = Random.Range(0, RoomPrefabs.Length);

        var Room = Instantiate(RoomPrefabs[StartRoom], FirstDoorway.transform.position, new Quaternion(0, 0, 0, 0), this.transform.parent);
        Room.GetComponent<RoomStats>().AmountOfRoomsLeft = RoomAmount--;
        Room.GetComponent<RoomStats>().dungionGenerator = this.gameObject;
        Room.GetComponent<RoomStats>().IsWayToExit = true;

        Room.GetComponent<RoomStats>().LoadingDungion();
    }
}
