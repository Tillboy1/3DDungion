using UnityEngine;

[CreateAssetMenu(fileName = "SheildingPresence", menuName = "Scriptable Objects/Tank/SheildingPresence")]
public class SheildingPresence : AOEAbilityBase
{
    public int sheildAdded;
    public override void Effect()
    {
        for (int i = 0; i < AllToEffect.Count; i++)
        {
            if (AllToEffect[i].GetComponent<PlayerStats>())
            {
                if (AllToEffect[i] == player && EffectsCaster || AllToEffect[i] != player)
                {
                    AllToEffect[i].GetComponent<PlayerStats>().Sheild.Value += sheildAdded;
                }
            }
        }
    }
}
