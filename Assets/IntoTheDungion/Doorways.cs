using UnityEngine;

public class Doorways : MonoBehaviour
{
    public int AmountOfRoomsLeft = 0;
    public bool IsWayToExit = false;

    public bool CheckIfBlocked()
    {
        RaycastHit hit;

        Physics.Raycast(this.transform.position, transform.forward, out hit, 10f);

        if (hit.collider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}