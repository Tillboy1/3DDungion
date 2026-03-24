using UnityEngine;

public class MassEnemy : BaseEnemy
{

    public virtual void Respawn()
    {
        currentHealth = maxHealth;
        this.gameObject.SetActive(true);
    }
}
