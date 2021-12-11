using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class core : MonoBehaviour
{
    private PhaseManager pm;
    private HealthBoss hb;
    public GameObject deathParticle;

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
        Instantiate(deathParticle, this.transform.position, this.transform.rotation);
        hb.deathEvent -= Die;
    }

    private void OnDestroy()
    {
        pm.listOfCores.Remove(this);
    }
}
