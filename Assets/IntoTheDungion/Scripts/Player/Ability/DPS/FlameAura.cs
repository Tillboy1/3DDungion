using UnityEngine;

public class FlameAura : AOEAbilityBase
{
    public int Damage;
    public override void Effect()
    {
        for (int i = 0; i < AllToEffect.Count; i++)
        {
            if (AllToEffect[i].GetComponent<PlayerStats>())
            {
                AllToEffect[i].GetComponent<PlayerStats>().TakeDamage(Damage);
            }
            else if (AllToEffect[i].GetComponent<BaseEnemy>())
            {
                AllToEffect[i].GetComponent<BaseEnemy>().TakeDamage(Damage, player);
            }
        }
    }
}
