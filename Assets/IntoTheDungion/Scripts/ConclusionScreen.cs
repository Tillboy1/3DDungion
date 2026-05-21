using UnityEngine;
using TMPro;

public class ConclusionScreen : MonoBehaviour
{
    public PlayerStats Player;

    public GameObject WinText;
    public GameObject lossText;

    public TMP_Text RemainingText;

    public TMP_Text BossText;
    public TMP_Text EnemyText;
    public TMP_Text MonGainedText;
    public TMP_Text MonLossText;
    public TMP_Text ProfitText;

    public int MoneyGained;
    public int MoneySpent;

    public void Start()
    {
        WinText = transform.GetChild(0).gameObject;
        WinText = transform.GetChild(1).gameObject;
        WinText = transform.GetChild(3).gameObject;
    }

    public void OpenConclusion(bool DidWeWin, float Remaining)
    {
        if (DidWeWin)
        {
            WinText.SetActive(true);
            lossText.SetActive(false);
        }
        else
        {
            WinText.SetActive(false);
            lossText.SetActive(true);
        }

        int min = Mathf.FloorToInt(Remaining / 60);
        int sec = Mathf.FloorToInt(Remaining % 60);

        RemainingText.text = string.Format("{0:00}:{1:00}", min, sec);

        BossText.text = DTime.instance.BossesKilled + " / " + DTime.instance.bosses.Count;
        EnemyText.text = DTime.instance.EnemiesKilled + " / " + DTime.instance.enemies.Count;

        MonGainedText.text = MoneyGained.ToString();
        MonLossText.text = MoneySpent.ToString();
        ProfitText.text = (MoneyGained - MoneySpent).ToString();
    }
}
