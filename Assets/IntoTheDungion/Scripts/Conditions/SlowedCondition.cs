using UnityEngine;

[CreateAssetMenu(fileName = "Slowed", menuName = "Scriptable Objects/Condition/Slowed")]
public class SlowedCondition : ConditionsBase
{
    public float OriginalSpeed;
    public float SetSpeed;
    public float Modifier;

    public override void Activate(GameObject Player)
    {
        OriginalSpeed = Player.GetComponent<PlayerMovement>().moveSpeed;
        SetSpeed = OriginalSpeed / Modifier;
    }
    public override void Deactivate(GameObject Player)
    {
        Player.GetComponent<PlayerMovement>().moveSpeed = OriginalSpeed;
    }
}
