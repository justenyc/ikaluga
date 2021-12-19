using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldeath : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;
    private HealthBoss hb;

    private void Start()
    {
        hb = this.GetComponent<HealthBoss>();
        hb.deathEvent += Die;
    }
    void Die()
    {
        Debug.Log("is this happening");
        Instantiate(deathParticles, this.transform.position, this.transform.rotation);
        hb.deathEvent -= Die;
    }
}
