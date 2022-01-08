using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* TODO
 * Music
 * Sound Effects
 * Death Screen
 * Health Bar
 * Win Screen
 * Adds
 * Attack after flag breaks
 * Lightning Strike after flag breaks
 */
public class Oni : MonoBehaviour
{
    [HideInInspector] public HealthBoss myHealth;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public GameObject oni;
    [HideInInspector] public HealthPlayer ph;
    public float stateChangeTimer = 10f;
    Flag flag; 

    OniInterface state;

    [Space(10)]
    public Vector3 direction;
    public float moveSpeed;
    public float turnSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        flag = FindObjectOfType<Flag>();
        flag.GetComponent<HealthBoss>().deathEvent += FlagListener;
        oni = GameObject.Find("Oni");
        ph = FindObjectOfType<HealthPlayer>();
        
        myHealth = this.GetComponent<HealthBoss>();
        myHealth.halfhealth += HalfHealthDance;

        anim = oni.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        ChangeFresnelColor(myHealth.bright);
        GameObject.Find("Ogre").GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("Vector1_cb68193afe724db7996723ccea4e88f6", 1);
        state = new OniIdle(this);
    }

    // Update is called once per frame
    void Update()
    {
        state.StateUpdate();
        //Debug.Log("Oni State: " + state);
    }

    void HalfHealthDance()
    {
        stateChangeTimer = 5f;
        state = new OniAttacking(this, 4);
        myHealth.halfhealth -= HalfHealthDance;
    }

    public void StateCountdown()
    {
        if (stateChangeTimer > 0)
            stateChangeTimer -= Time.deltaTime;
        else
        {
            
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

    public void FacePlayer()
    {
        TrackPlayer();
        float xDistance = (transform.position.x + direction.x) - transform.position.x;
        float zDistance = (transform.position.z + direction.z) - transform.position.z;
        float angle = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
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
        float angle = Mathf.Atan2(xDistance, zDistance) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0,angle,0)), Time.deltaTime * turnSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "wellwall")
        {
            direction *= -1;
        }
    }

    public void FlagListener()
    {
        flag.GetComponent<HealthBoss>().deathEvent -= FlagListener;
        state = new OniPassive(this);
    }

    public void ChangeState(OniInterface newState)
    {
        state = newState;
    }
}
