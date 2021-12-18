using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class deathcanvas : MonoBehaviour
{
    private fade fadeScript;

    private void Start()
    {
        fadeScript = FindObjectOfType<fade>();
    }

    public void InvokeReloadScene()
    {
        Invoke("ReloadScene", 1.1f);
        fadeScript.FadeStart();
    }

    public void InvokeHub()
    {
        Invoke("BackToHub", 1.1f);
        fadeScript.FadeStart();
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToHub()
    {
        SceneManager.LoadScene(5);
    }
}
