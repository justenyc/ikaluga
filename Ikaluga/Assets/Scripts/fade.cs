using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeEnd()
    {
        animator.SetBool("Fade", false);
    }

    public void FadeStart()
    {
        animator.SetBool("Fade", true);
    }
}
