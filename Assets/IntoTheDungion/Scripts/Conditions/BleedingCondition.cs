using UnityEngine;

[CreateAssetMenu(fileName = "Bleeding", menuName = "Scriptable Objects/Condition/Bleeding")]
public class BleedingCondition : ConditionsBase
{
    public int BleedDamage;

    public void Bleeding(GameObject Player)
    {
        if (Player.GetComponent<PlayerStats>())
        {
            Player.GetComponent<PlayerStats>().CurrentHealth.Value -= BleedDamage;
        }
        else
        {
            Player.GetComponent<BaseEnemy>().currentHealth.Value -= BleedDamage;
        }
    }
}
