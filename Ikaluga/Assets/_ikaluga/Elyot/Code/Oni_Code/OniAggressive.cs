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
        manager.moveSpeed = 15;
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
