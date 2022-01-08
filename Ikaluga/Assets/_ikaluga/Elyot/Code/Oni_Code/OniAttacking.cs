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
        int r = Random.Range(1, 11);
        Debug.Log(r);
        if (r > 5)
            manager.myHealth.bright = !manager.myHealth.bright;

        manager.FacePlayer();
        if (manager.GetComponent<HealthBoss>().currentHealth <= manager.GetComponent<HealthBoss>().maxHealth / 2)
            manager.anim.SetInteger("AnimState", 3);// Random.Range(1, 4));
        else
            manager.anim.SetInteger("AnimState", 3);//Random.Range(1, 3));
    }

    public void StateUpdate()
    {
        //Anim event sets AnimState to 0
        if(manager.anim.GetInteger("AnimState") < 1)
        {
            if (manager.myHealth.currentHealth <= manager.myHealth.maxHealth / 2)
            {
                manager.ChangeState(new OniAggressive(manager));
            }
            else
            {
                manager.ChangeState(new OniPassive(manager));
            }
        }
    }
}
