using UnityEngine;

[CreateAssetMenu(fileName = "DamageBoost", menuName = "Scriptable Objects/DPS/DamageBoost")]
public class DammageBoost : AbilitiesBase
{
    public int ExtraDamage;
    public override void Activate(GameObject player)
    {
        Debug.Log("Add Extra Damage of " + ExtraDamage);
    }
}
