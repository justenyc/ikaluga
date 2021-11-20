using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool bright = false;

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void DealDamage(float value)
    {
        currentHealth -= value;
        Debug.Log(this.name + " Current Health = " + currentHealth);
        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
