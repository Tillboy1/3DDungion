using UnityEngine;
using UnityEngine.UI;

public class ArmourHolder : MonoBehaviour
{
    public ArmourBase HeldArmour;
    public CharacterSheet CharacterSheet;

    private void Start()
    {
        //this.GetComponent<Image>().sprite = HeldWeapon.sprite;
    }
    public void ClickingBTN()
    {
        CharacterSheet.UpdateArmourInfo(HeldArmour);
    }
}
