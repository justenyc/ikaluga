using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniPassive : OniInterface
{
    Oni manager;
    float attackTimer;

    // Start is called before the first frame update
    public OniPassive(Oni managerRef)
    {
        manager = managerRef;
        attackTimer = Random.Range(3f, 7f);
        StateStart();
    }
    
    public void StateStart()
    {
        manager.moveSpeed = 1;
        manager.RandomDirection();
    }

    // Update is called once per frame
    public void StateUpdate()
    {
        if (attackTimer <= 0)
        {
            manager.ChangeState(new OniAttacking(manager));            
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
        Movement();
    }

    void Movement()
    {
        manager.Rotate();
        manager.rb.position += manager.transform.forward * manager.moveSpeed * Time.deltaTime;
        manager.anim.SetFloat("Move", manager.moveSpeed);
    }
}
