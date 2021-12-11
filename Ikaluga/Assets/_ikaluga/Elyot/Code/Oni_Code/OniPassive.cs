using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniPassive : OniInterface
{
    Oni manager;
    // Start is called before the first frame update
    public OniPassive(Oni managerRef)
    {
        manager = managerRef;
        StateStart();
    }
    
    public void StateStart()
    {
        
    }

    // Update is called once per frame
    public void StateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        manager.TrackPlayer();
        manager.Rotate();
        manager.rb.position += manager.direction * manager.moveSpeed * Time.deltaTime;
        manager.anim.SetFloat("Move", manager.moveSpeed);
    }
}
