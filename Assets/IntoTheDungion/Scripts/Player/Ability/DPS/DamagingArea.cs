using UnityEngine;


[CreateAssetMenu(fileName = "DamagingArea", menuName = "Scriptable Objects/DPS/DamagingArea")]
public class DamagingArea : AOEAbilityBase
{
    public int Damage;
    public override void Effect()
    {
        Debug.Log(AllToEffect.Count);

        for (int i = 0; i < AllToEffect.Count; i++)
        {
            Debug.Log("got to count of " + i);
            if (AllToEffect[i].GetComponent<PlayerStats>())
            {
                if (AllToEffect[i] == player && EffectsCaster || AllToEffect[i] != player)
                {
                    AllToEffect[i].GetComponent<PlayerStats>().TakeDamage(Damage);
                }
            }
            else if (AllToEffect[i].GetComponent<BaseEnemy>())
            {
                AllToEffect[i].GetComponent<BaseEnemy>().TakeDamage(Damage, player);
            }
        }
    }
}
