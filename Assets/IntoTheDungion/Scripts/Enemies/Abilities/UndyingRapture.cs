using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "UndyingRapture", menuName = "Scriptable Objects/Enemy/UndyingRapture")]
public class UndyingRapture : AbilitiesBase
{
    public PoisonedCondtion PoisonedCondtion;

    public override void Activate(GameObject player)
    {
        Debug.Log("Activated");
        PoisonedCondtion.CauserOfEffliction = player;
        Debug.Log(player.GetComponent<BossEnemy>().player);
        player.GetComponent<BossEnemy>().player.GetComponent<PlayerStats>().CurrentConditions.Add(PoisonedCondtion);
    }
}
