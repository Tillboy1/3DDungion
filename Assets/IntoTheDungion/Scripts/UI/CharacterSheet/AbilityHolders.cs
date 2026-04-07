using UnityEngine;
using UnityEngine.UI;

public class AbilityHolders : MonoBehaviour
{
    public AbilitiesBase HeldAbility;
    public CharacterSheet CharacterSheet;

    private void Start()
    {
        this.GetComponent<Image>().sprite = HeldAbility.sprite;
    }
    public void ClickingBTN()
    {
        CharacterSheet.UpdateAbilityInfo(HeldAbility);
    }
}
