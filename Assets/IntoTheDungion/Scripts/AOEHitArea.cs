using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class AOEHitArea : MonoBehaviour
{
    [Header("Base Requirements")]
    public GameObject Creater;
    public AOEAbilityBase abilityConnected;
    public Collider collid;
    public List<GameObject> ObjectsinArea;

    public float TimeToDestroy;
    public float TimeBetweenEffects;
    public bool BetweenEffects;

    public bool AbleToLeave;
    public float ScaleAmount;
    public bool WillFollow;

    private void Start()
    {
        collid = this.GetComponent<Collider>();
    }
    private void Update()
    {
        if (!BetweenEffects)
        {
            StartCoroutine(Effect());
        }
        if (this.transform.localScale.x < ScaleAmount || this.transform.localScale.y < ScaleAmount || this.transform.localScale.z < ScaleAmount)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + 0.1f * Time.deltaTime, this.transform.localScale.y + 0.1f * Time.deltaTime, this.transform.localScale.z + 0.1f * Time.deltaTime);
        }
        if (WillFollow)
        {
            if (abilityConnected.SpawnOnPointer)
            {
                this.transform.position = Creater.GetComponent<PlayerMovement>().hitPosition;
            }
            else
            {
                this.transform.position = Creater.transform.position;
            }
        }
    }
    public void Activate()
    {
        StartCoroutine(startedCountdown());
    }

    private void OnTriggerEnter(Collider other)
    {
        ObjectsinArea.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!AbleToLeave)
        {
            ObjectsinArea.Remove(other.gameObject);
        }
    }

    IEnumerator startedCountdown()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(this.gameObject);
    }
    IEnumerator Effect()
    {
        BetweenEffects = true;
        Debug.Log("Effect Started");
        abilityConnected.CheckAOE();
        yield return new WaitForSeconds(TimeBetweenEffects);
        BetweenEffects = false;
    }
}
