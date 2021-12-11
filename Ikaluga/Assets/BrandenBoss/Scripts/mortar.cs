using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortar : MonoBehaviour
{
    private float shotDelay;
    public float minDelay;
    public float maxDelay;

    public Transform shotPoint;
    public GameObject mortarPreFab;
    private HealthBoss hb;
    public GameObject deathParticle;

    private PhaseManager pm;
    // Start is called before the first frame update
    void Start()
    {
        hb = this.GetComponent<HealthBoss>();
        hb.deathEvent += Die;
        shotDelay = Random.Range(minDelay, maxDelay);
        pm = FindObjectOfType<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.mortarsOn == true)
        {
            shotDelay -= Time.deltaTime;
            if (shotDelay <= 0)
            {
                FireMortar();
            }
        }
    }
    void Die()
    {
        Instantiate(deathParticle, this.transform.position, this.transform.rotation);
        hb.deathEvent -= Die;
    }
    void FireMortar()
    {
        Instantiate(mortarPreFab, shotPoint.position, shotPoint.rotation);
        shotDelay = Random.Range(minDelay, maxDelay);
    }
    private void OnDestroy()
    {
        pm.listOfMortars.Remove(this);
    }
}
