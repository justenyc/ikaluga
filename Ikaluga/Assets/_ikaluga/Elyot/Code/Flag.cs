using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public AudioSource BGM;
    // Start is called before the first frame update
    private void OnDestroy()
    {
        BGM.gameObject.SetActive(true);
        BGM.Play();
        BGM.loop = true;
    }
}
