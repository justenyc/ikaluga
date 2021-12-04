using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pillar : MonoBehaviour
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
        pm.listOfPillars.Remove(this);
    }
}
