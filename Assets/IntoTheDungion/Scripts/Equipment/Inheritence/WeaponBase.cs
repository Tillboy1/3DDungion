using UnityEngine;

public enum StatRequireed
{
    Strength,
    Dexterity,
    Intelligence,
    Wisdom,
    Charisma
}
public class WeaponBase : ScriptableObject
{
    [Header("Basics")]
    public string Name;
    public string Description;
    public Sprite sprite;

    [Header("Weapon")]
    public int Damage;
    public int CritBounus;
    public StatRequireed statUsed;
    public int StatAmountRequired;

    public float AttackRange;
}
