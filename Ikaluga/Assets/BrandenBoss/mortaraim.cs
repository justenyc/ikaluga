using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mortaraim : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float lifetime = 1f;
    public bool bright = true;
    public float damage;
    public float trackTime;
    public GameObject originObject;
    private Transform target;
    private Vector3 trackedPosition;

    [Space(10)]
    public Color lightMainColor;
    [ColorUsage(true, true)] public Color lightEmissiveColor;
    public Color darkMainColor;
    [ColorUsage(true, true)] public Color darkEmissiveColor;

    public MeshRenderer mr;

    // Update is called once per frame
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        mr = this.GetComponent<MeshRenderer>();
        Destroy(this.gameObject, lifetime);
        ChangeColor(bright); 
    }

    void Update()
    {
        trackTime -= Time.deltaTime;

        if (trackTime > 0)
        {
            transform.position += transform.up * moveSpeed * Time.deltaTime;
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            trackedPosition = target.transform.position;
        }
        else
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, trackedPosition + Vector3.up, step);
        }
    }

    //Called by instantiating object
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

    public void ChangeDamage(float f)
    {
        damage = f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Projectile: " + collision.collider.gameObject);
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
                Destroy(this.gameObject);
            }
            catch
            {
                Destroy(this.gameObject);
            }
        }
    }
}
