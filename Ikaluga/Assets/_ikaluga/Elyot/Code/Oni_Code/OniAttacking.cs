using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniAttacking : OniInterface
{
    Oni manager;
    public OniAttacking(Oni managerRef)
    {
        manager = managerRef;
        RandomlyChangeColor();
        StateStart();
    }

    public OniAttacking(Oni managerRef, int forceAnimState)
    {
        manager = managerRef;
        manager.FacePlayer();
        manager.anim.SetInteger("AnimState", forceAnimState);
    }

    public void StateStart()
    {
        manager.GetComponent<Rigidbody>().velocity = Vector3.zero;
        manager.FacePlayer();
        if (manager.GetComponent<HealthBoss>().currentHealth <= manager.GetComponent<HealthBoss>().maxHealth / 2)
            manager.anim.SetInteger("AnimState", Random.Range(1, 4));
        else
            manager.anim.SetInteger("AnimState", Random.Range(1, 3));
    }

    public void StateUpdate()
    {
        //Anim event sets AnimState to 0
        if(manager.anim.GetInteger("AnimState") < 1)
        {
            if (manager.myHealth.currentHealth <= manager.myHealth.maxHealth * 0.3f)
            {
                manager.ChangeState(new OniAggressive(manager));
            }
            else
            {
                manager.ChangeState(new OniPassive(manager));
            }
        }
    }

    void RandomlyChangeColor()
    {
        int r = Random.Range(1, 11);
        if (r > 5)
            manager.myHealth.bright = !manager.myHealth.bright;
    }
}
