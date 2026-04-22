using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AOEAbilityBase : AbilitiesBase
{
    [Header("AOE")]
    public List<GameObject> AllToEffect;
    public GameObject player;
    public GameObject AOEPrefab;
    public GameObject CurrentAOE;
    public bool EffectsCaster = false, SpawnOnPointer = false;
    
    public float AOEDuration;
    public float AOEBetweenEffects;

    public float ScaleAmount = 1;
    public bool AbleToLeave;
    public bool FollowsAfter;

    public override void Activate(GameObject Player)
    {
        var GO = Instantiate(AOEPrefab);

        CurrentAOE = GO;
        AOEHitArea AOEHA = GO.GetComponent<AOEHitArea>();

        player = Player;
        AOEHA.Creater = Player;
        AOEHA.abilityConnected = this;
        AOEHA.TimeToDestroy = AOEDuration;
        AOEHA.TimeBetweenEffects = AOEBetweenEffects;

        AOEHA.AbleToLeave = AbleToLeave;
        AOEHA.ScaleAmount = ScaleAmount;
        AOEHA.WillFollow = FollowsAfter;

        if (Player.GetComponent<PlayerStats>())
        {
            PlayerStats stats = Player.GetComponent<PlayerStats>();

            if (SpawnOnPointer)
            {
                GO.transform.position = stats.GetComponent<PlayerMovement>().hitPosition;
            }
            else
            {
                GO.transform.position = Player.transform.position;
            }
        }
        AOEHA.Activate();
    }

    public void CheckAOE()
    {
        #region Checks whats in the region
        if (AbleToLeave)
        {
            AllToEffect.Clear();
            AllToEffect = CurrentAOE.GetComponent<AOEHitArea>().ObjectsinArea;
        }
        else
        {
            for (int i = 0; i < CurrentAOE.GetComponent<AOEHitArea>().ObjectsinArea.Count; i++)
            {
                Debug.Log(CurrentAOE.GetComponent<AOEHitArea>().ObjectsinArea[i].name);
                if (!AllToEffect.Contains(CurrentAOE.GetComponent<AOEHitArea>().ObjectsinArea[i]))
                {
                    Debug.Log("Added Item");
                    AllToEffect.Add(CurrentAOE.GetComponent<AOEHitArea>().ObjectsinArea[i]);
                }
            }
        }
        #endregion

        Effect();
    }
    public virtual void Effect()
    {

    }
}
