using UnityEngine;

[CreateAssetMenu(fileName = "Dash", menuName = "Scriptable Objects/DPS/Dash")]
public class Dash : AbilitiesBase
{
    public override void Activate()
    {
        Debug.Log("Dashing");
        player.GetComponent<Rigidbody>().linearVelocity = new Vector3(player.GetComponent<PlayerMovement>().MouseLocation.x - player.transform.position.x, 0, player.GetComponent<PlayerMovement>().MouseLocation.y - player.transform.position.y);
    }
}
