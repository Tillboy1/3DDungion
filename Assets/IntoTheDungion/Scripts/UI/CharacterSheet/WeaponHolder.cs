using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    public WeaponBase HeldWeapon;
    public CharacterSheet CharacterSheet;

    private void Start()
    {
        //this.GetComponent<Image>().sprite = HeldWeapon.sprite;
    }
    public void ClickingBTN()
    {
        CharacterSheet.UpdateWeaponInfo(HeldWeapon);
    }
}
