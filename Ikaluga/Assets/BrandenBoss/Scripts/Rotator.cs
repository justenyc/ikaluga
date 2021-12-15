using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotateSpeed;
    public float rotateTime;
    public float minTime;
    public float maxTime;
    public float stopTime;

    private bool rotatingForward;
    private bool stopped;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        rotateTime -= Time.deltaTime;
        if (rotatingForward && !stopped)
        {
            RotateForward();
        }
        else if (!rotatingForward && !stopped)
        {
            RotateBackward();
        }

        if(rotateTime <= 0)
        {
            if (!stopped)
            {
                Stop();
            }
        }
    }

    void RotateForward()
    {
        transform.Rotate(0, Time.deltaTime * rotateSpeed, 0, Space.Self);
    }

    void RotateBackward()
    {
        transform.Rotate(0, -Time.deltaTime * rotateSpeed, 0, Space.Self);
    }

    void Stop()
    {
        stopped = true;
        transform.Rotate(0, 0, 0, Space.Self);
        Invoke("ResetTime", stopTime);
    }

    void ResetTime()
    {
        stopped = false;
        if (rotatingForward)
        {
            rotatingForward = false;
        }
        else if (!rotatingForward)
        {
            rotatingForward = true;
        }
        rotateTime = Random.Range(minTime, maxTime);
    }
}
