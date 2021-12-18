using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniIdle : OniInterface
{
    Oni manager;
    public OniIdle(Oni managerRef)
    {
        manager = managerRef;
        StateStart();
    }

    // Start is called before the first frame update
    public void StateStart()
    {
        
    }

    // Update is called once per frame
    public void StateUpdate()
    {
        
    }
}
