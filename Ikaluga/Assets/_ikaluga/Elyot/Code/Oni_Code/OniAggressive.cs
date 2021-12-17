using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniAggressive : OniInterface
{
    Oni manager;
    public OniAggressive(Oni managerRef)
    {
        manager = managerRef;
    }
    
    public void StateStart()
    {

    }

    public void StateUpdate()
    {
        Debug.Log("Oni is now aggressive");
    }
}
