using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    private TMP_Text TimerText;

    private void Awake()
    {
        TimerText = GetComponentInChildren<TMP_Text>();
    }
    private void Update()
    {
        int min = Mathf.FloorToInt(DTime.instance.Remaining / 60);
        int sec = Mathf.FloorToInt(DTime.instance.Remaining % 60);

        TimerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }
}
