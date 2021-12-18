using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar : MonoBehaviour
{
    private PhaseManager pm;
    public GameObject deathParticles;
    private HealthBoss hb;

    // Start is called before the first frame update
    void Start()
    {
        hb = this.GetComponent<HealthBoss>();
        hb.deathEvent += Die;
        pm = FindObjectOfType<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Die()
    {
        Instantiate(deathParticles, this.transform.position, this.transform.rotation);
        hb.deathEvent -= Die;
    }
    private void OnDestroy()
    {
        pm.listOfPillars.Remove(this);
    }
}
