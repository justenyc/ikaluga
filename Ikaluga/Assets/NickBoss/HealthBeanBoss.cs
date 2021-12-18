using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBeanBoss : HealthBoss
{
    public GameObject BossDieManager;
    public GameObject HealthText;

    public override void Die()
    {
        BossDieManager.GetComponent<BossDieScript>().HeartDied();
        Destroy(this.gameObject);
    }

    public override void DealDamage(float value)
    {

        currentHealth -= value;
        Debug.Log(this.name + " Current Health = " + currentHealth);
        if (currentHealth <= 0)
            Die();

        HealthText.GetComponent<TextMesh>().text = currentHealth.ToString();

    }
}
