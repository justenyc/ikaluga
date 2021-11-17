using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missile : Projectile
{
    public float upTime = 2.5f;

    private Transform target;

    // Update is called once per frame
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mr = this.GetComponent<MeshRenderer>();
        Destroy(this.gameObject, lifetime);
        this.GetComponent<HealthBoss>().bright = bright;
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
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }

    //Called by instantiating object


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
                Destroy(this.gameObject);
            }
            catch
            {
                
            }
        }
    }
}
