using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onething : MonoBehaviour
{

    private Transform target;
    private float speed = .1f;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        //var step = speed * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, step);
        transform.LookAt(target);
    }
}
