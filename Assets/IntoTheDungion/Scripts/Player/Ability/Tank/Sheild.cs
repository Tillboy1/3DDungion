using UnityEngine;

[CreateAssetMenu(fileName = "Sheild", menuName = "Scriptable Objects/Tank/Sheild")]
public class Sheild : AbilitiesBase
{
    public int ShildAmount;
    public override void Activate(GameObject player)
    {
        PlayerStats stats = player.GetComponent<PlayerStats>();

        stats.Sheild.Value = ShildAmount * CurrentLevel;
    }
}
