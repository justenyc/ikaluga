using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryListener : MonoBehaviour
{
    public HealthBoss boss;
    public GameObject winstuff;
    public GameObject BGM;

    private void Start()
    {
        boss.deathEvent += Victory;
    }

    void Victory()
    {
        winstuff.SetActive(true);
        BGM.SetActive(false);
        Flag[] flags = FindObjectsOfType<Flag>();
        foreach (Flag f in flags)
        {
            f.gameObject.SetActive(false);
        }
    }
}
