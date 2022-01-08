using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public AudioSource BGM;
    public bool fireAttack;
    public GameObject attack;
    public float attackTimer = 20f;
    public float attackTimerCountdown;

    private void Start()
    {
        attackTimerCountdown = attackTimer;
    }

    private void Update()
    {
        if (attackTimerCountdown > 0)
        {
            attackTimerCountdown -= Time.deltaTime;
        }
        else
        {
            if (fireAttack)
            {
                Instantiate(attack, transform.position, Quaternion.Euler(new Vector3(180,0,0)));
                attackTimerCountdown = attackTimer;
            }
        }
    }

    private void OnDestroy()
    {
        if (BGM != null)
        {
            BGM.gameObject.SetActive(true);
            BGM.Play();
            BGM.loop = true;
        }
    }
}
