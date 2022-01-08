using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swirl : MonoBehaviour
{
    public Projectile[] projectiles;
    public float waitTime = 1f;
    HealthBoss boss;

    // Start is called before the first frame update
    void Start()
    {
        SetHealthBoss();
        StartCoroutine(DelayActivate(waitTime));
    }

    private void Update()
    {
        if (projectiles.Length < 1)
            Destroy(this.gameObject);
    }

    IEnumerator DelayActivate(float delayTime)
    {
        foreach (Projectile go in projectiles)
        {
            go.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
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
    }

    public void SetProjColor(HealthBoss hb)
    {
        projectiles = GetComponentsInChildren<Projectile>();
        foreach (Projectile p in projectiles)
            p.gameObject.SetActive(false);

        try
        {
            foreach (Projectile p in projectiles)
            {
                p.ChangeColor(hb.bright);
            }
        }
        catch
        {

        }
    }
}
