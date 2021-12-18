using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walldeath : MonoBehaviour
{
    [SerializeField] GameObject deathParticles;

    private void OnDestroy()
    {
        Instantiate(deathParticles, this.transform.position, this.transform.rotation);
    }
}
