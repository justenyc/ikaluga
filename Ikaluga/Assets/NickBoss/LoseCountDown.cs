using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCountDown : MonoBehaviour
{

    public float LifeTime = 0.1f;
    private float CurrentLifeTime = 0.0f;

    public GameObject BossDieManager;
    public GameObject CountDownText;

    // Start is called before the first frame update
    void Start()
    {
        CountDownText.GetComponent<TextMesh>().text = "You lose if this reaches " + LifeTime.ToString("0.00") + ": " + CurrentLifeTime.ToString("0.00");
    }

    // Update is called once per frame
    void Update()
    {
        CurrentLifeTime = CurrentLifeTime + Time.deltaTime;
        CountDownText.GetComponent<TextMesh>().text = "Time limit: " + LifeTime.ToString("0.00") + " Time: " + CurrentLifeTime.ToString("0.00");

        if (CurrentLifeTime >= LifeTime)
        {
            BossDieManager.GetComponent<BossDieScript>().EndGameBossWins();
        }
    }
}
