using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniAggressive : OniInterface
{
    Oni manager;
    HealthPlayer hp;
    float attackTimer;
    public OniAggressive(Oni managerRef)
    {
        manager = managerRef;
        hp = manager.ph;
        StateStart();
    }
    
    public void StateStart()
    {
        int r = Random.Range(0, 11);
        if (r <= 5)
            manager.moveSpeed = 15f;
        else
            manager.moveSpeed = 5.5f;
        
        manager.turnSpeed = 3f;
        attackTimer = Random.Range(3f, 7f);
    }

    public void StateUpdate()
    {
        Movement();

        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;
        try
        {
            if (Vector3.Distance(hp.transform.position, manager.transform.position) < 10f || attackTimer <= 0)
            {
                manager.ChangeState(new OniAttacking(manager));
            }
        }
        catch
        {

        }
    }

    void Movement()
    {
        manager.TrackPlayer();
        manager.Rotate();
        manager.rb.position += manager.transform.forward * manager.moveSpeed * Time.deltaTime;
        manager.anim.SetFloat("Move", manager.moveSpeed);
    }
}
