using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;

    private float shotDelay; //delay between shots
    public float minDelay;
    public float maxDelay;


    // Start is called before the first frame update
    void Start()
    {
        shotDelay = Random.Range(minDelay, maxDelay);
    }

    // Update is called once per frame
    void Update()
    {
        shotDelay -= Time.deltaTime;
        if(shotDelay <= 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        Instantiate(bullet, shotPoint.position, shotPoint.rotation);
        shotDelay = Random.Range(minDelay, maxDelay);
    }
}
