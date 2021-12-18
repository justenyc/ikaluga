using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portalwarp : MonoBehaviour
{
    public SceneEnum scene;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("is this happening");
        if (collision.CompareTag("Player"))
        {
            
            switch (scene)
            {
                case SceneEnum.Hub:
                    SceneManager.LoadScene(5);
                    break;
                case SceneEnum.BrandenBoss:
                    SceneManager.LoadScene(2);
                    break;
                case SceneEnum.NickBoss:
                    SceneManager.LoadScene(3);
                    break;
                case SceneEnum.ElyotBoss:
                    SceneManager.LoadScene(4);
                    break;
                case SceneEnum.JustenBoss:
                    //this doesnt exist yet
                    break;
            }
        }
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
