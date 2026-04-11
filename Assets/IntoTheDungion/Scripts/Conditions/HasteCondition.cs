using UnityEngine;

[CreateAssetMenu(fileName = "Haste", menuName = "Scriptable Objects/Condition/Haste")]
public class HasteCondition : ConditionsBase
{
    public float OriginalSpeed;
    public float SetSpeed;
    public float Modifier;

    public override void Activate(GameObject Player)
    {
        OriginalSpeed = Player.GetComponent<PlayerMovement>().moveSpeed;
        SetSpeed = OriginalSpeed * Modifier;
        Player.GetComponent<PlayerMovement>().moveSpeed = SetSpeed;
        //Also Going to add Attack Speed
    }
    public override void Deactivate(GameObject Player)
    {
        Player.GetComponent<PlayerMovement>().moveSpeed = OriginalSpeed;
        //Return Attack Speed
    }
}
