using UnityEngine;

public class TeamUIManager : MonoBehaviour
{
    private void FixedUpdate()
    {
        foreach (Transform objects in transform)
        {
            objects.gameObject.SetActive(false);
        }
        for (int i = 0; i < PlayerManager.instance.Players.Count; i++)
        {
            if (i <= PlayerManager.instance.Players.Count)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
                this.transform.GetChild(i).gameObject.GetComponent<HealthBar>().Player = PlayerManager.instance.Players[i].GetComponent<PlayerStats>();
            }
        }
    }
}
