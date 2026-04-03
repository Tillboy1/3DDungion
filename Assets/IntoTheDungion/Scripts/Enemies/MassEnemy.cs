using UnityEngine;

public class MassEnemy : BaseEnemy
{
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public virtual void Respawn()
    {
        currentHealth = maxHealth;
        this.gameObject.SetActive(true);
    }
}
