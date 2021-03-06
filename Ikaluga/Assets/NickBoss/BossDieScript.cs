using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class BossDieScript : MonoBehaviour
{
    public GameObject player;

    public GameObject LosingScreen;
    public GameObject VictoryScreen;
    public GameObject TimelineCube;
    public GameObject VictoryPortal;
    public GameObject Countdown;
    public GameObject Player;

    public GameObject BeanBoss;
    public GameObject[] ExplodeObjects;

    public GameObject ExplosionEffect;

    public int HeartCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HeartDied()
    {
        HeartCount = HeartCount - 1;
        if (HeartCount <= 0)
        {
            EndGamePlayerWins();
        }
    }

    public void EndGamePlayerWins()
    {
        Destroy(TimelineCube);
        Destroy(Countdown);
        Invoke("DestroyStage", 3);

        VictoryScreen.SetActive(true);
        Debug.Log("GAME ENDED ALL BOSS HEARTS DEAD");
    }

    public void EndGameBossWins()
    {
        Player.GetComponent<HealthPlayer>().Die();
        Cursor.lockState = CursorLockMode.Confined;
        Destroy(Countdown);
        LosingScreen.SetActive(true);
        Debug.Log("GAME ENDED PLAYER DEAD");
    }

    void DestroyStage()
    {
        foreach (GameObject currentObj in ExplodeObjects)
        {
            Debug.Log(currentObj.transform);
            Instantiate(ExplosionEffect, currentObj.transform.position, transform.rotation);
            Destroy(currentObj);
        }
        BeanBoss.GetComponent<Rigidbody>().AddForce(new Vector3(10, 10, 0));
        VictoryPortal.SetActive(true);
    }
}

public class ListUtil
{
    public ObservableCollection<GameObject> myList;
    public BossDieScript ender_script;
    public bool isPlayerTracker;

    //The constructor 
    public ListUtil(BossDieScript ender_script, bool isPlayerTracker)
    {
        this.myList = new ObservableCollection<GameObject>();
        this.myList.CollectionChanged += myList_CollectionChanged;
        this.ender_script = ender_script;
        this.isPlayerTracker = isPlayerTracker;
    }

    void myList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        //list changed - an item was removed.
        if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
        {
            if (myList.Count == 0)
            {
                if (isPlayerTracker)
                {
                    ender_script.EndGameBossWins();
                }
                else
                {
                    ender_script.EndGamePlayerWins();
                }
            }
        }
    }

}
