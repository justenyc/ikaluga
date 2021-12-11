using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public bool bright = false;

    [ColorUsage(true, true)] public Color brightColor = new Color(0.5f, 0.5f, 0.2509804f);
    [ColorUsage(true, true)] public Color darkColor = new Color(0.1254902f, 0, 0.5019608f);

    public void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void DealDamage(float value)
    {
        currentHealth -= value;
        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
