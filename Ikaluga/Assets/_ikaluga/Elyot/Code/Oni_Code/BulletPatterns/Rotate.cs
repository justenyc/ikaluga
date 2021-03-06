using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float lifeTime = 20f;
    public float rotationSpeed = 1f;
    public Vector3 axes = Vector3.zero;

    private void Start()
    {
        if (lifeTime != 0)
            Destroy(this.gameObject, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(new Vector3(rot.x + axes.x * rotationSpeed * Time.deltaTime, rot.y + axes.y * rotationSpeed * Time.deltaTime, rot.z + axes.z * rotationSpeed * Time.deltaTime));
    }
}
