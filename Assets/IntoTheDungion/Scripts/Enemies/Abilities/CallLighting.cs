using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "CallLighting", menuName = "Scriptable Objects/Enemy/CallLighting")]
public class CallLighting : AbilitiesBase
{
    public GameObject AOE;

    public override void Activate(GameObject Player)
    {
        Debug.Log("Lighting");
        float ranX = Random.Range(-0.75f, 0.7f); 
        float ranY = Random.Range(-1.0f, 0.2f); 
        var go = Instantiate(AOE, Player.transform.parent);
        go.transform.localPosition = new Vector3(ranX, 4, ranY);
    }
}
