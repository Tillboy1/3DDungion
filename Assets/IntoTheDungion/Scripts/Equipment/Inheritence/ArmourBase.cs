using UnityEngine;

public class ArmourBase : ScriptableObject
{
    [Header("Basics")]
    public string Name;
    public string Description;
    public Sprite sprite;

    [Header("Armour")]
    public int ArmourBounus;
    public int StrengthRequired;
}
