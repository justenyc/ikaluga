using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryListener : MonoBehaviour
{
    public HealthBoss boss;
    public GameObject winstuff;

    private void Start()
    {
        boss.deathEvent += Victory;
    }

    void Victory()
    {
        winstuff.SetActive(true);
    }
}
