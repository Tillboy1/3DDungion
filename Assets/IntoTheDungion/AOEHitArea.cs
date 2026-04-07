using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEHitArea : MonoBehaviour
{
    [Header("Base Requirements")]
    public GameObject Caller;
    public Collider collid;
    public List<GameObject> ObjectsinArea;

    public float TimeToDestroy;
    public bool AbleToLeave;
    public float ScaleAmount;

    private void Start()
    {
        collid = this.GetComponent<Collider>();
    }
    private void Update()
    {
        if (this.transform.localScale.x < ScaleAmount || this.transform.localScale.y < ScaleAmount || this.transform.localScale.z < ScaleAmount)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + 0.1f * Time.deltaTime, this.transform.localScale.y + 0.1f * Time.deltaTime, this.transform.localScale.z + 0.1f * Time.deltaTime);
        }
    }
    public void Activate()
    {

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
        yield return new WaitForSeconds(TimeToDestroy + 0.01f);
        Destroy(this.gameObject);
    }
}
