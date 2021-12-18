using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swirl : MonoBehaviour
{
    public Projectile[] projectiles;
    public float waitTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        projectiles = GetComponentsInChildren<Projectile>();
        foreach (Projectile p in projectiles)
            p.gameObject.SetActive(false);

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
}
