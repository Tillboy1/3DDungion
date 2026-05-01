using UnityEngine;

[CreateAssetMenu(fileName = "ReArmour", menuName = "Scriptable Objects/Tank/ReArmour")]
public class ReArmour : AbilitiesBase
{
    public override void Activate(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();
        if (stats.ArmourCurrent.Value + stats.ArmourTotal.Value / (10 - CurrentLevel) >= stats.ArmourTotal.Value)
        {
            stats.ArmourCurrent.Value = stats.ArmourTotal.Value;
        }
        else
        {
            stats.ArmourCurrent.Value += stats.ArmourTotal.Value / (10 - CurrentLevel);
        }
    }
}
