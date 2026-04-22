using UnityEngine;

[CreateAssetMenu(fileName = "HealingArea", menuName = "Scriptable Objects/Support/HealingArea")]
public class HealingArea : AOEAbilityBase
{
    public int AmountHealed;
    public override void Effect()
    {
        for (int i = 0; i < AllToEffect.Count; i++)
        {
            if (AllToEffect[i].GetComponent<PlayerStats>())
            {
                if (AllToEffect[i] == player && EffectsCaster || AllToEffect[i] != player)
                {
                    AllToEffect[i].GetComponent<PlayerStats>().BaseHeal(AmountHealed);
                }
            }
        }
    }
}
