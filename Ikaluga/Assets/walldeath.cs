using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldeath : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;
    private HealthBoss hb;

    private void Start()
    {
        hb.deathEvent += Die;
    }
    void Die()
    {
        Instantiate(deathParticles, this.transform.position, this.transform.rotation);
        Debug.Break();
        hb.deathEvent -= Die;
    }
}
