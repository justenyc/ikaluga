using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Projectile[] projectiles;
    HealthBoss boss;

    // Start is called before the first frame update
    void Start()
    {
        SetHealthBoss();
        projectiles = GetComponentsInChildren<Projectile>();
        foreach (Projectile p in projectiles)
            p.gameObject.SetActive(false);

        SetProjColor(boss);
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

    void SetProjColor(HealthBoss hb)
    {
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
