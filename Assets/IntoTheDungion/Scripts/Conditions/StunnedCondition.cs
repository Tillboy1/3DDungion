using UnityEngine;

[CreateAssetMenu(fileName = "StunnedCondition", menuName = "Scriptable Objects/Condition/StunnedCondition")]
public class StunnedCondition : ConditionsBase
{
    public override void Activate(GameObject Player)
    {
        if (Player.GetComponent<PlayerStats>())
        {
            Player.GetComponent<PlayerStats>().AbleToMove = false;
        }
        else
        {
            Player.GetComponent<BaseEnemy>().AbleToMove = false;
        }
    }
    public override void Deactivate(GameObject Player)
    {
        if (Player.GetComponent<PlayerStats>())
        {
            Player.GetComponent<PlayerStats>().AbleToMove = true;
        }
        else
        {
            Player.GetComponent<BaseEnemy>().AbleToMove = true;
        }
    }
}
