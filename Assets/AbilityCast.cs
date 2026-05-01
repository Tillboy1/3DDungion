using UnityEngine;
using Unity.Netcode;

/*
public enum AbilityState
{
    Ready,
    Casting,
    Undergoing,
    Cooldown
}
*/
public class AbilityCast : NetworkBehaviour
{
    public NetworkVariable<float> CurrentCastingCalldown;
    public NetworkVariable<float> CurrentActiveCalldown;
    public NetworkVariable<float> CurrentResetCalldown;

    public AbilityState status;
    public NetworkVariable<int> CurrentLevel = new NetworkVariable<int>(1);
    public NetworkVariable<int> CurrentEX;

    public AbilitiesBase details;
    public NetworkVariable<bool> isCasting = new NetworkVariable<bool>(true);

    public void Start()
    {
        details.ACaster = this;
    }

    public void Update()
    {
        if (HasAuthority)
        {
            switch (status)
            {
                case AbilityState.Ready:
                    // Done with input actions
                    break;
                case AbilityState.Casting:
                    isCasting.Value = true;
                    if (CurrentCastingCalldown.Value > 0)
                    {
                        CurrentCastingCalldown.Value -= Time.deltaTime;
                    }
                    else
                    {
                        details.Activate(this.gameObject);
                        CurrentActiveCalldown.Value = details.RefreshTime;
                        status = AbilityState.Undergoing;
                    }
                    break;
                case AbilityState.Undergoing:
                    if (CurrentActiveCalldown.Value > 0)
                    {
                        CurrentActiveCalldown.Value -= Time.deltaTime;
                    }
                    else
                    {
                        isCasting.Value = false;
                        CurrentResetCalldown.Value = details.RefreshTime;
                        status = AbilityState.Cooldown;
                    }
                    break;
                case AbilityState.Cooldown:
                    if (CurrentResetCalldown.Value > 0)
                    {
                        CurrentResetCalldown.Value -= Time.deltaTime;
                    }
                    else
                    {
                        status = AbilityState.Ready;
                    }
                    break;
            }
        }
    }

    [Rpc(SendTo.Server)]
    public void ActivateRpc()
    {
        if (isCasting.Value != false)
        {
            details.Activate(this.transform.parent.transform.parent.gameObject);
            status = AbilityState.Casting;
        }
    }
}
