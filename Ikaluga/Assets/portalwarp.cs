using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalwarp : MonoBehaviour
{
    public SceneEnum scene;
    private fade fadeScript;

    private void Start()
    {
        fadeScript = FindObjectOfType<fade>();

    }

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("is this happening");
        if (collision.CompareTag("Player"))
        {
            
            switch (scene)
            {
                case SceneEnum.Hub:
                    Invoke("LoadHub", 1.1f);
                    fadeScript.FadeStart();
                    break;
                case SceneEnum.BrandenBoss:
                    Invoke("LoadBrandenBoss", 1.1f);
                    fadeScript.FadeStart();
                    break;
                case SceneEnum.NickBoss:
                    Invoke("LoadNickBoss", 1.1f);
                    fadeScript.FadeStart();
                    break;
                case SceneEnum.ElyotBoss:
                    Invoke("LoadElyotBoss", 1.1f);
                    fadeScript.FadeStart();
                    break;
                case SceneEnum.JustenBoss:
                    //this doesnt exist yet
                    break;
            }
        }
    }
    void LoadHub()
    {
        SceneManager.LoadScene(5);
    }

    void LoadBrandenBoss()
    {
        SceneManager.LoadScene(2);
    }

    void LoadNickBoss()
    {
        SceneManager.LoadScene(3);
    }

    void LoadElyotBoss()
    {
        SceneManager.LoadScene(4);
    }
}



public enum SceneEnum
{
    Hub,
    BrandenBoss,
    NickBoss,
    ElyotBoss,
    JustenBoss
};
