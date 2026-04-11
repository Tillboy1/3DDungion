using UnityEngine;

[CreateAssetMenu(fileName = "NonHeal", menuName = "Scriptable Objects/Condition/NonHeal")]
public class NonHealCondition : ConditionsBase
{
    public override void Activate(GameObject Player)
    {
        if (Player.GetComponent<PlayerStats>())
        {
            Player.GetComponent<PlayerStats>().AbleToHeal = false;
        }
        else
        {
            Player.GetComponent<BaseEnemy>().AbleToHeal = false;
        }
    }
    public override void Deactivate(GameObject Player)
    {
        if (Player.GetComponent<PlayerStats>())
        {
            Player.GetComponent<PlayerStats>().AbleToHeal = true;
        }
        else
        {
            Player.GetComponent<BaseEnemy>().AbleToHeal = true;
        }
    }
}
