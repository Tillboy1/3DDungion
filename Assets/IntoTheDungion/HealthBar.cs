using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerStats Player;

    public Image PlayerSprite = null;

    public Slider CharacterHealth = null;
    public Slider CharacterXP = null;

    public TMP_Text CharName = null;
    public TMP_Text CharLevel = null;

    private void Start()
    {
        foreach (Transform Childs in transform)
        {
            if (Childs.GetComponent<Image>())
            {
                PlayerSprite = Childs.gameObject.GetComponent<Image>();
            }
            else if (Childs.gameObject.name == "Health Slider")
            {
                CharacterHealth = Childs.gameObject.GetComponent<Slider>();
            }
            else if (Childs.gameObject.name == "XP Slider")
            {
                CharacterXP = Childs.gameObject.GetComponent<Slider>();
            }
            else if (Childs.gameObject.name == "Chacter Name Text")
            {
                CharName = Childs.gameObject.GetComponent<TMP_Text>();
            }
            else if (Childs.gameObject.name == "Level Text")
            {
                CharLevel = Childs.gameObject.GetComponent<TMP_Text>();
            }
        }
    }

    private void Update()
    {
        Debug.Log("Update");
        SetInfo();
    }
    public void SetInfo()
    {
        Debug.Log(Player.CurrentHealth.Value / Player.maxHealth.Value + "=" + Player.CurrentHealth.Value + "/" + Player.maxHealth.Value);

        CharacterHealth.value = Player.CurrentHealth.Value / Player.maxHealth.Value;
        CharacterXP.value = Player.CurrentXp.Value / Player.RequiredXp.Value;
        //CharName.text = Player.gameObject.name;
        CharLevel.text = Player.CurrentLevel.Value.ToString();
    }
}
