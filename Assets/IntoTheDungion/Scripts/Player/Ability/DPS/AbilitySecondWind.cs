using UnityEngine;

[CreateAssetMenu(fileName = "Second Wind", menuName = "Scriptable Objects/DPS/Second Wind")]
public class AbilitySecondWind : AbilitiesBase
{
    [Header("Ability Variables")]
    public int HealthGained;

    public override void Activate(GameObject player)
    {
        player.GetComponent<PlayerStats>().BaseHeal(HealthGained);
    }
}
