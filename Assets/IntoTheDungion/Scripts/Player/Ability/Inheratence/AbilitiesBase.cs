using System.Collections;
using UnityEngine;

public class AbilitiesBase : ScriptableObject
{
    [Header("Basics")]
    public string Name;
    public string Description;
    public string ClassRequired;
    public Sprite sprite;

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
    public int CurrentLevel = 1;
    public int MaxLevel = 10;

    public int CurrentEX;
    public int EXPToLevel = 1;
    public int[] EXPDifficulties;
    public int[] LvlAddition;

    public virtual void Activate(GameObject Player) {}
    public void AbilityAddXP()
    {
        if (CurrentLevel != MaxLevel)
        {
            if (EXPToLevel == CurrentEX++)
            {
                CurrentEX = 0;
                CurrentLevel++;

                // increases the xp required
                for(int i = 0; i < EXPDifficulties.Length; i++)
                {
                    if(CurrentLevel == EXPDifficulties[i])
                    {
                        EXPToLevel++;
                    }
                }
            }
        }
    }
}
