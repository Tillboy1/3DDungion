using UnityEngine;

public class AbilityHolders : MonoBehaviour
{
    public AbilitiesBase HeldAbility;
    public CharacterSheet CharacterSheet;

    public void ClickingBTN()
    {
        CharacterSheet.UpdateAbilityInfo(HeldAbility);
    }
}
