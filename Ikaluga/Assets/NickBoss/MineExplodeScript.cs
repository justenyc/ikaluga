using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplodeScript : MonoBehaviour
{
    public float lifetime;
    public float damage;
    public float explosion_radius;
    private float existence_time = 0f;
    public GameObject explosion_effect;
    private GameObject player;

    public bool bright = true;

    [Space(10)]
    public Color lightMainColor;
    [ColorUsage(true, true)] public Color lightEmissiveColor;
    public Color darkMainColor;
    [ColorUsage(true, true)] public Color darkEmissiveColor;

    public MeshRenderer mr;

    // Start is called before the first frame update
    void Start()
    {
        mr = this.GetComponent<MeshRenderer>();
        ChangeColor(bright, mr);
    }

    // Update is called once per frame
    void Update()
    {
        existence_time = existence_time + Time.deltaTime;

        if (existence_time > lifetime)
        {
            Explode();
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(explosion_effect, transform.position, transform.rotation);
        explosion.transform.localScale = new Vector3(explosion_radius * 2, explosion_radius * 2, explosion_radius * 2);
        
        MeshRenderer explodeEffectMR = explosion.GetComponent<MeshRenderer>();
        ChangeColor(bright, explodeEffectMR);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosion_radius);

        foreach(Collider nearbyObject in colliders)
        {
            HealthPlayer health = nearbyObject.GetComponent<HealthPlayer>();
            if (health != null)
            {
                health.DealDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    //Called by instantiating object
    public void ChangeColor(bool b, MeshRenderer givenMR)
    {
        if (givenMR == null)
        {
            givenMR = this.GetComponent<MeshRenderer>();
        }

        bright = b;
        if (bright)
        {
            givenMR.material.SetColor("EmissiveColor", lightEmissiveColor);
            givenMR.material.SetColor("MainColor", lightMainColor);
        }
        else
        {
            givenMR.material.SetColor("EmissiveColor", darkEmissiveColor);
            givenMR.material.SetColor("MainColor", darkMainColor);
        }
    }

}
