using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missileaim : MonoBehaviour
{

    private float shotDelay;
    public float minDelay;
    public float maxDelay;

    public Transform shotPoint;
    public GameObject missile;

    private PhaseManager pm;
    // Start is called before the first frame update
    void Start()
    {
        shotDelay = Random.Range(minDelay, maxDelay);
        pm = FindObjectOfType<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pm.missilesOn == true)
        {
            shotDelay -= Time.deltaTime;
            if (shotDelay <= 0)
            {
                FireMissile();
            }
        }
    }

    void FireMissile()
    {
        Instantiate(missile, shotPoint.position, shotPoint.rotation);
        shotDelay = Random.Range(minDelay, maxDelay);
    }
    private void OnDestroy()
    {
        pm.listOfMissiles.Remove(this);
    }
}
