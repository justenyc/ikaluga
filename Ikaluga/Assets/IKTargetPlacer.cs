using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetPlacer : MonoBehaviour
{
    public Transform followCamera;
    public Transform player;
    public LayerMask mask;
    // Update is called once per frame
    private void Awake()
    {
        transform.parent = null;
    }

    void Update()
    {
        Vector3 direction = transform.position - player.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        RaycastHit hit;

        if (Physics.Raycast(followCamera.transform.position, followCamera.transform.forward, out hit, 100f, mask))
        {
            transform.position = hit.point;
        }
    }
}
