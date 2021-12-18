using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatetitle : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Vector3 axes = Vector3.zero;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(rot.x + axes.x * rotationSpeed * Time.deltaTime, rot.y + axes.y * rotationSpeed * Time.deltaTime, rot.z + axes.z * rotationSpeed * Time.deltaTime));
    }
}
