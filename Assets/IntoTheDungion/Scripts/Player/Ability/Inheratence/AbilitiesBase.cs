using System.Collections;
using UnityEngine;

public class AbilitiesBase : ScriptableObject
{
    [Header("Basics")]
    public string Name;
    public string Description;
    public string ClassRequired;

    [Header("Casting")]
    public float CastingTime;
    public float RefreshTime;

    public AbilityState AbilityState;

    public float RemainingCasting;
    public float RemainingRefresh;

    [Header("Leveling UP")]
    public int CurrentLevel = 1;
    public int MaxLevel = 10;

    public int CurrentEX;
    public int EXPToLevel = 1;
    public int[] EXPDifficulties;

    public virtual void Activate(GameObject Player) {}

    public void AbilityRegiven()
    {
        if (CurrentLevel != MaxLevel)
        {
            if (EXPToLevel == CurrentEX++)
            {
                CurrentEX = 0;

                // increases the xp required
                for(int i = 0; i < EXPDifficulties.Length; i++)
                {
                    if(CurrentLevel == EXPDifficulties[i])
                    {
                        EXPToLevel++;
                    }
                }
                CurrentLevel++;
            }
        }
    }
}
