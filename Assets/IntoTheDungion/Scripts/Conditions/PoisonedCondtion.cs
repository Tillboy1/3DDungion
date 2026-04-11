using UnityEngine;

[CreateAssetMenu(fileName = "Poisoned", menuName = "Scriptable Objects/Condition/Poisoned")]
public class PoisonedCondtion : ConditionsBase
{
    public int damageInflicted;
    public PlayerStats IsPlayer;
    public BaseEnemy IsEnemy;


    public override void Activate(GameObject Player)
    {
        if (Player.GetComponent<PlayerStats>())
        {
            IsPlayer = Player.GetComponent<PlayerStats>();
        }
        else
        {
            IsEnemy = Player.GetComponent<BaseEnemy>();
        }
    }
    public override void IfOnUpdate(GameObject Player)
    {

        if (IsPlayer)
        {
            IsPlayer.TakeDamage(damageInflicted);
        }
        else
        {
            IsEnemy.TakeDamage(damageInflicted);
        }
    }
}
