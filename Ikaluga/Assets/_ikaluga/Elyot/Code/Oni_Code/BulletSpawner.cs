using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    /*public GameObject bullet;
    public HealthBoss hb;
    public Transform[] bulletSpawners;
    public void FireBullet(float damage)
    {
        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        Projectile p = b.GetComponent<Projectile>();
        p.ChangeColor(hb.bright);
        p.ChangeDamage(damage);
        p.originObject = hb.gameObject;
    }*/

    public void OnEnable()
    {
        Debug.Log("BulletSpawner enabled");
    }
}
