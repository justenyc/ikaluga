using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniAttacking : OniInterface
{
    Oni manager;
    public OniAttacking(Oni managerRef)
    {
        manager = managerRef;
        StateStart();
    }

    public OniAttacking(Oni managerRef, int forceAnimState)
    {
        manager = managerRef;
        manager.anim.SetInteger("AnimState", 4);
    }

    public void StateStart()
    {
        manager.FacePlayer();
        if (manager.GetComponent<HealthBoss>().currentHealth <= manager.GetComponent<HealthBoss>().maxHealth / 2)
            manager.anim.SetInteger("AnimState", 1);// Random.Range(1, 4));
        else
            manager.anim.SetInteger("AnimState", 1);//Random.Range(1, 3));
    }

    public void StateUpdate()
    {
        if(manager.anim.GetInteger("AnimState") < 1)
        {
            manager.ChangeState(new OniPassive(manager));
        }
    }
}
