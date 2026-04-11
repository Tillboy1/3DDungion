using UnityEngine;

[CreateAssetMenu(fileName = "RedirectDamage", menuName = "Scriptable Objects/Condition/RedirectDamage")]
public class RedirectDamageCondition : ConditionsBase
{
    public GameObject persontodirect;
    public int MaxDamageTransfer;
    public bool LimitToTransfer;
    public bool CanReturnExtra;

    public int DamageTransfer(int Damage)
    {
        if (!LimitToTransfer)
        {
            if (persontodirect.GetComponent<PlayerStats>())
            {
                persontodirect.GetComponent<PlayerStats>().TakeDamage(Damage);
                return 0;
            }
            else
            {
                persontodirect.GetComponent<BaseEnemy>().TakeDamage(Damage);
                return 0;
            }
        }
        else
        {
            if(Damage > MaxDamageTransfer)
            {
                if (persontodirect.GetComponent<PlayerStats>())
                {
                    persontodirect.GetComponent<PlayerStats>().TakeDamage(MaxDamageTransfer);

                    return Damage - MaxDamageTransfer;
                }
                else
                {
                    persontodirect.GetComponent<BaseEnemy>().TakeDamage(MaxDamageTransfer);
                    return Damage - MaxDamageTransfer;
                }
            }
            else
            {
                if (persontodirect.GetComponent<PlayerStats>())
                {
                    persontodirect.GetComponent<PlayerStats>().TakeDamage(Damage);
                    return 0;
                }
                else
                {
                    persontodirect.GetComponent<BaseEnemy>().TakeDamage(Damage);
                    return 0;
                }
            }
        }
    }
}
