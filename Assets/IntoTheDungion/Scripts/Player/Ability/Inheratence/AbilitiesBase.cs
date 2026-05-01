using System.Collections;
using UnityEngine;

public class AbilitiesBase : ScriptableObject
{
    [Header("Basics")]
    public string Name;
    public string Description;
    public string ClassRequired;
    public Sprite sprite;

    public AbilityCast ACaster;

    [Header("Casting")]
    public float CastingTime;
    public float DurationTime;
    public float RefreshTime;

    public AbilityState AbilityState;

    public float RemainingCasting;
    public float RemainingDuration;
    public float RemainingRefresh;

    public int AbilityRange;

    [Header("Leveling UP")]
    public int MaxLevel = 10;

    public int EXPToLevel = 1;
    public int[] EXPDifficulties;
    public int[] LvlAddition;

    public virtual void Activate(GameObject Player) {}
    public void AbilityAddXP()
    {
        if (ACaster.CurrentLevel.Value != MaxLevel)
        {
            if (EXPToLevel >= ACaster.CurrentEX.Value++)
            {
                ACaster.CurrentEX.Value -= EXPToLevel;
                ACaster.CurrentLevel.Value++;

                // increases the xp required
                for(int i = 0; i < EXPDifficulties.Length; i++)
                {
                    if(ACaster.CurrentLevel.Value == EXPDifficulties[i])
                    {
                        EXPToLevel++;
                    }
                }
            }
        }
    }
}
