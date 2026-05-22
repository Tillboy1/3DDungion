using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : BaseEnemy
{
    public AbilitiesBase BossAbility;

    protected override void Update()
    {
        base.Update();
        if (BossAbility != null)
        {
            switch (BossAbility.AbilityState)
            {
                case AbilityState.Ready:
                    // Done with input actions
                    break;
                case AbilityState.Casting:
                    if (BossAbility.RemainingCasting > 0)
                    {
                        BossAbility.RemainingCasting -= Time.deltaTime;
                    }
                    else
                    {
                        BossAbility.Activate(this.gameObject);
                        BossAbility.RemainingDuration = BossAbility.DurationTime;
                        BossAbility.AbilityState = AbilityState.Undergoing;
                    }
                    break;
                case AbilityState.Undergoing:
                    if (BossAbility.RemainingDuration > 0)
                    {
                        BossAbility.RemainingDuration -= Time.deltaTime;
                    }
                    else
                    {
                        BossAbility.RemainingRefresh = BossAbility.RefreshTime;
                        BossAbility.AbilityState = AbilityState.Cooldown;
                    }
                    break;
                case AbilityState.Cooldown:
                    if (BossAbility.RemainingRefresh > 0)
                    {
                        BossAbility.RemainingRefresh -= Time.deltaTime;
                    }
                    else
                    {
                        BossAbility.AbilityState = AbilityState.Ready;
                    }
                    break;
            }
        }
    }
    public override void AttackPlayer()
    {
        base.AttackPlayer();
        if (BossAbility != null)
        {
            if (BossAbility.AbilityState == AbilityState.Ready)
            {
                Debug.Log("UsedAbility");
                BossAbility.AbilityState = AbilityState.Casting;
            } 
        }
    }
}
