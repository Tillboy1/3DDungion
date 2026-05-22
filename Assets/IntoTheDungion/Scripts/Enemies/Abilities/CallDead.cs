using UnityEngine;

[CreateAssetMenu(fileName = "CallDead", menuName = "Scriptable Objects/Enemy/CallDead")]
public class CallDead : AbilitiesBase
{
    public GameObject DeadToSummon;

    public override void Activate(GameObject Player)
    {
        var go = Instantiate(DeadToSummon, Player.transform.parent);
        go.transform.localPosition = new Vector3(0, 4, 0);
    }
}
