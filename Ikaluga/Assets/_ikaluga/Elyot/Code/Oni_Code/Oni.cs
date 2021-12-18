using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oni : MonoBehaviour
{
    [HideInInspector] public HealthBoss myHealth;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public GameObject oni;
    [HideInInspector] public HealthPlayer ph;
    public float stateChangeTimer = 10;
    public float aggression = 7;

    OniInterface state;

    [Space(10)]
    public Vector3 direction;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        oni = GameObject.Find("Oni");
        ph = FindObjectOfType<HealthPlayer>();
        myHealth = this.GetComponent<HealthBoss>();
        anim = oni.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        ChangeFresnelColor(myHealth.bright);
        GameObject.Find("Ogre").GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("Vector1_cb68193afe724db7996723ccea4e88f6", 1);
        state = new OniIdle(this);
    }

    // Update is called once per frame
    void Update()
    {
        //ChangeState();
        state.StateUpdate();
    }

    void ChangeState()
    {
        if (stateChangeTimer > 0)
            stateChangeTimer -= Time.deltaTime;
        else
        {
            float r = 0;// Random.Range(0, 10);
            if (r > aggression)
            {
                state = new OniAggressive(this);
            }
            else
            {
                state = new OniPassive(this);
            }
            stateChangeTimer = Random.Range(5f, 15f);
        }
    }
   
    public void ChangeFresnelColor(bool b)
    {
        if (b)
        {
            GameObject.Find("Ogre").GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("MainColor", myHealth.brightColor);
        }
        else
        {
            GameObject.Find("Ogre").GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("MainColor", myHealth.darkColor);
        }
    }

    public void TrackPlayer()
    {
        direction = new Vector3(ph.transform.position.x - transform.position.x, 0, ph.transform.position.z - transform.position.z).normalized;
    }

    public void RandomDirection()
    {
        Vector2 randomDirection = Random.insideUnitCircle;
        direction = new Vector3(randomDirection.x,0,randomDirection.y).normalized;
    }

    public void Rotate()
    {
        float xDistance = (transform.position.x + direction.x) - transform.position.x;
        float zDistance = (transform.position.z + direction.z) - transform.position.z;
        float angle = Mathf.Atan2(zDistance, xDistance) * Mathf.Rad2Deg;
        angle -= 90f;
        transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wellwall")
        {
            direction *= -1;
        }
    }

    public void Attack(string s)
    {

    }
}
