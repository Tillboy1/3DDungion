using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable Objects/DPS/Dash")]
public class Dash : AbilitiesBase
{
    public float DashForce;
    public float DashForceUpward;
    

    public override void Activate(GameObject player)
    {
        Debug.Log("Dashing");
        Vector3 forceToApply = player.GetComponent<Transform>().right * DashForce + player.GetComponent<Transform>().up * DashForceUpward;
        player.GetComponent<Rigidbody>().AddForce(forceToApply, ForceMode.Impulse);
    }
}
