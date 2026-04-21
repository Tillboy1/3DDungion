using System.Collections;
using System.Collections.Generic;
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
    //public bool isavbe;

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
    }
    public void Activate()
    {
        StartCoroutine(startedCountdown());
    }
    public void Returninfo()
    {
        //Caller
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
        Returninfo();
        yield return new WaitForSeconds(0.01f);
        Destroy(this.gameObject);
    }
    IEnumerator Effect()
    {
        BetweenEffects = true;
        abilityConnected.CheckAOE();
        yield return new WaitForSeconds(TimeBetweenEffects);
        BetweenEffects = false;
    }
}
