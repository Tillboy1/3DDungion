using UnityEngine;

[CreateAssetMenu(fileName = "Regeneration", menuName = "Scriptable Objects/Condition/Regeneration")]
public class RegenerationCondition : ConditionsBase
{
    public int HealthPerBurst;
    public PlayerStats IsPlayer;
    public BaseEnemy IsEnemy;

    public void Awake()
    {
        OnUpdate = true;
    }

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
            IsPlayer.BaseHeal(HealthPerBurst);
        }
        else
        {
            IsEnemy.BaseHeal(HealthPerBurst);
        }
    }
}
