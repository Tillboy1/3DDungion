using UnityEngine;
using Unity.Netcode;

public class AbilityCast : NetworkBehaviour
{
    NetworkVariable<float> CurrentCastingCalldown;
    NetworkVariable<float> CurrentActiveCalldown;
    NetworkVariable<float> CurrentResetCalldown;
    [SerializeField]
    AbilitiesBase details;

    [Rpc(SendTo.Server)]
    public void ActivateRpc()
    {
        if (CurrentCastingCalldown.Value == 0)
        {
            CurrentCastingCalldown.Value = details.RefreshTime;
        }
        if (CurrentActiveCalldown.Value == 0)
        {
            CurrentActiveCalldown.Value = details.RefreshTime;
        }
        if (CurrentResetCalldown.Value == 0)
        {
            CurrentResetCalldown.Value = details.RefreshTime;
        }
    }
}
