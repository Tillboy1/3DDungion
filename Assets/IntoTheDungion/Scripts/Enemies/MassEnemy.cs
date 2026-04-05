using UnityEngine;

public class MassEnemy : BaseEnemy
{
    private void Start()
    {
        currentHealth.Value = maxHealth;
    }
    public virtual void Respawn()
    {
        currentHealth.Value = maxHealth;
        this.gameObject.SetActive(true);
    }
}
