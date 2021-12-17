using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : Projectile
{
    public float upTime = 2.5f;

    private Transform target;
    private SphereCollider sc;
    public GameObject particles;
    private HealthBoss hb;

    // Update is called once per frame
    private void Start()
    {
        hb = this.GetComponent<HealthBoss>();
        hb.deathEvent += Die;
        sc = GetComponent<SphereCollider>();
        sc.enabled = false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mr = this.GetComponent<MeshRenderer>();
        Destroy(this.gameObject, lifetime);
        this.GetComponentInParent<HealthBoss>().bright = bright;
        ChangeColor(bright);
    }

    void Update()
    {
        
        upTime -= Time.deltaTime;

        if(upTime > 0)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
        }
        else
        {
            sc.enabled = true;
            float step = moveSpeed/5 * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.position = Vector3.Lerp(transform.position, target.position, step);
        }
        

    }

    //Called by instantiating object
    void Die()
    {
        Instantiate(particles, this.transform.position, this.transform.rotation);
        hb.deathEvent -= Die;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Missile: " + collision.collider.name);
        if (collision.collider.gameObject != this.originObject)
        {
            try
            {
                HealthPlayer collidedHp = collision.collider.GetComponent<HealthPlayer>();
                if (collidedHp.bright != this.bright)
                {
                    collidedHp.DealDamage(damage);
                }
                else
                {
                    collidedHp.DealDamage(0);
                }
                Destroy(this.gameObject);
            }
            catch
            {
                
            }
        }
    }

    private void OnDestroy()
    {
        Instantiate(particles, this.transform.position, this.transform.rotation);
    }
}
