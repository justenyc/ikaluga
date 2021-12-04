using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAim : MonoBehaviour
{
    public Transform target;
    public bool playerSighted;
    Vector3 startPos;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = this.transform.position;
    }

    void Update()
    {
        if (playerSighted == true)
        {
            transform.LookAt(target);
        }
        else if(playerSighted == false)
        {
            this.transform.position = startPos;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerSighted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerSighted = false;
        }
    }
}
