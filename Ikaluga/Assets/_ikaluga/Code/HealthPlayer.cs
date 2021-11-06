using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer : Health
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        try
        {
            Projectile p = hit.collider.GetComponent<Projectile>();
            if (p.originObject != this.gameObject)
            {
                DealDamage(p.damage);
                Destroy(p.gameObject);
            }
        }
        catch
        {

        }
    }
}
