using UnityEngine;

public class AOEBoss : MonoBehaviour
{
    public int damage;
    public int TimeAlive;
    public float RemainingLife;

    public void Start()
    {
        RemainingLife = TimeAlive;
    }
    public void Update()
    {
        if (RemainingLife > 0)
        {
            RemainingLife -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerStats>() != null)
        {
            Debug.Log("we just did " + damage + " to the player");
            other.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
