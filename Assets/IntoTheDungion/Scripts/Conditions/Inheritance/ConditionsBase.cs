using UnityEngine;

public class ConditionsBase : ScriptableObject
{

    [Header("Basics")]
    public string Name;
    public string Description;
    public Sprite sprite;
    public bool OnUpdate;
    public bool betweenseconds;

    [Header("Timers")]
    public float DelayTime;
    public float TotalTime;

    public AbilityState State;

    public float RemainingDelay;
    public float RemainingTime;

    public virtual void Activate(GameObject Player) { }
    public virtual void IfOnUpdate(GameObject Player) { }
    public virtual void Deactivate(GameObject Player) { }
}
