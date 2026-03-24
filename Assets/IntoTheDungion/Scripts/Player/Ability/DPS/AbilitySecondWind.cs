using UnityEngine;

[CreateAssetMenu(fileName = "Second Wind", menuName = "Scriptable Objects/DPS/Second Wind")]
public class AbilitySecondWind : AbilitiesBase
{
    [Header("Ability Variables")]
    public int HealthGained;

    public override void Activate()
    {
        player.GetComponent<PlayerStats>().BaseHeal(HealthGained);
    }
}
