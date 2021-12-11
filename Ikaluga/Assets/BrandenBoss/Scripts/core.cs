using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class core : MonoBehaviour
{
    private PhaseManager pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PhaseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        pm.listOfCores.Remove(this);
    }
}
