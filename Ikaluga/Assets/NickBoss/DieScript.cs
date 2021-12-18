using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{
    public float LifeTime = 0.1f;

    private float CurrentLifeTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLifeTime = CurrentLifeTime + Time.deltaTime;

        if (CurrentLifeTime >= LifeTime)
        {
            Destroy(gameObject);
        }
    }
}
