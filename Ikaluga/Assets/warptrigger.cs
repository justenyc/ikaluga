using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class warptrigger : MonoBehaviour
{
    public SceneEnum scene;
    public GameObject warpCanvas;
    public TextMeshProUGUI bossNameText;

    public string hubText;
    public string brandenBossText;
    public string nickBossText;
    public string elyotBossText;
    public string justenBossText;

    //this script handles the trigger canvas specifically, bad name lol

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (scene)
            {
                case SceneEnum.Hub:
                    warpCanvas.SetActive(true);
                    bossNameText.text = hubText;
                    break;
                case SceneEnum.BrandenBoss:
                    warpCanvas.SetActive(true);
                    bossNameText.text = brandenBossText;
                    break;
                case SceneEnum.NickBoss:
                    warpCanvas.SetActive(true);
                    bossNameText.text = nickBossText;
                    break;
                case SceneEnum.ElyotBoss:
                    warpCanvas.SetActive(true);
                    bossNameText.text = elyotBossText;
                    break;
                case SceneEnum.JustenBoss:
                    warpCanvas.SetActive(true);
                    bossNameText.text = justenBossText;
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            warpCanvas.SetActive(false);
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
}

