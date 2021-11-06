using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateBackward();
    }

    void RotateForward()
    {
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
    }

    void RotateBackward()
    {
        transform.Rotate(0, -Time.deltaTime * rotateSpeed, 0, Space.Self);
    }
}
