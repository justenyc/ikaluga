using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public float damage = 10f;
    public List<Health> list = new List<Health>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name + " has entered water");
        if (other.GetComponent<Health>())
            list.Add(other.GetComponent<Health>());
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.name + " has exited water");
        if (other.GetComponent<Health>())
            list.Remove(other.GetComponent<Health>());
    }

    public void Shock()
    {
        foreach(Health h in list)
        {
            h.DealDamage(damage);
        }
    }
}