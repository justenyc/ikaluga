using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;
    private PhaseManager pm;
    public GameObject deathParticle;

    private float shotDelay; //delay between shots
    public float minDelay;
    public float maxDelay;

    public float startMinDelay;
    public float startMaxDelay;
    private HealthBoss hb;


    // Start is called before the first frame update
    void Start()
    {
        hb = this.GetComponent<HealthBoss>();
        hb.deathEvent += Die;
        shotDelay = Random.Range(startMinDelay, startMaxDelay);
        pm = FindObjectOfType<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.turretsOn == true)
        {
            shotDelay -= Time.deltaTime;
            if (shotDelay <= 0)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        shotDelay = Random.Range(minDelay, maxDelay);
    }

    void Die()
    {
        Instantiate(deathParticle, this.transform.position, this.transform.rotation);
        hb.deathEvent -= Die;
    }

    private void OnDestroy()
    {
        pm.listOfTurrets.Remove(this);
    }
}
