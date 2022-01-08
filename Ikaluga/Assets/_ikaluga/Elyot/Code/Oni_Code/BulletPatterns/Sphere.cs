using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Projectile[] projectiles;
    public HealthBoss boss;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetHealthBoss()
    {
        try
        {
            boss = FindObjectOfType<Oni>().GetComponent<HealthBoss>();
        }
        catch
        {

        }
    }

    public void SetHealthBoss(HealthBoss hb)
    {
        try
        {
            boss = hb;
        }
        catch
        {

        }
        projectiles = GetComponentsInChildren<Projectile>();
        foreach (Projectile p in projectiles)
            p.gameObject.SetActive(false);
    }

    public void SetProjColor(HealthBoss hb)
    {
        projectiles = GetComponentsInChildren<Projectile>();

        try
        {
            foreach (Projectile p in projectiles)
            {
                p.ChangeColor(hb.bright);
                p.originObject = hb.gameObject;
                p.gameObject.SetActive(true);
            }
        }
        catch
        {
            foreach (Projectile p in projectiles)
            {
                p.gameObject.SetActive(true);
            }
        }
    }
}
