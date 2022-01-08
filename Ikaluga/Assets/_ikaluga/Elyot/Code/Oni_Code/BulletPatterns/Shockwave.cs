using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : Projectile
{
    // Start is called before the first frame update
    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
        //ChangeColor(bright);
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject, 1f);
            this.GetComponent<MeshRenderer>().enabled = false;
            this.GetComponent<Collider>().enabled = false;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(20, 20, 20), 1/lifetime * Time.deltaTime);
    }

    public void ChangeColor(bool b)
    {
        if (mr == null)
        {
            mr = this.GetComponent<MeshRenderer>();
        }

        bright = b;
        if (bright)
        {
            mr.material.SetColor("EmissiveColor", lightEmissiveColor);
            mr.material.SetColor("MainColor", lightMainColor);
        }
        else
        {
            mr.material.SetColor("EmissiveColor", darkEmissiveColor);
            mr.material.SetColor("MainColor", darkMainColor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Shockwave: " + collision.collider.gameObject);
        if (collision.collider.gameObject != this.originObject)
        {
            try
            {
                Health collidedHp = collision.collider.GetComponent<Health>();
                if (collidedHp.bright != bright)
                {
                    if (collidedHp.GetComponent<missile>() != null && this.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
                    {
                        collidedHp.DealDamage(0);
                    }
                    else if (collidedHp.GetComponent<missile>() != null && this.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
                    {
                        collidedHp.DealDamage(damage);
                    }
                    else if (collidedHp.GetComponent<missile>() == null)
                    {
                        collidedHp.DealDamage(damage);
                    }
                }
                else
                {
                    collidedHp.DealDamage(0);
                }
            }
            catch
            {

            }
        }
        this.GetComponent<SphereCollider>().enabled = false;
    }
}
