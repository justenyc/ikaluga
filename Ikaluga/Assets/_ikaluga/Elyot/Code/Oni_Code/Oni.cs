using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* TODO
 * Sound Effects
 * Adds
 * Attack after flag breaks
 * Lightning Strike after flag breaks
 */
public class Oni : MonoBehaviour
{
    [HideInInspector] public HealthBoss myHealth;
    public Animator anim;
    [HideInInspector] public Rigidbody rb;
    public GameObject oni;
    [HideInInspector] public HealthPlayer ph;
    public float stateChangeTimer = 10f;
    public Flag startFlag; 

    OniInterface state;

    [Space(10)]
    public Vector3 direction;
    public float moveSpeed;
    public float turnSpeed = 1;
    public Minion[] minions;
    public GameObject flag;
    public GameObject flagSpinner;

    // Start is called before the first frame update
    void Start()
    {
        startFlag.GetComponent<HealthBoss>().deathEvent += FlagListener;
        ph = FindObjectOfType<HealthPlayer>();
        
        myHealth = this.GetComponent<HealthBoss>();
        myHealth.phaseOne += PhaseOneDance;
        myHealth.phaseTwo += PhaseTwoDance;

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

    void PhaseTwoDance()
    {
        stateChangeTimer = 5f;
        state = new OniAttacking(this, 4);
        Instantiate(flag, transform.position + new Vector3(0, 10, 0), Quaternion.identity, this.transform);
        myHealth.phaseTwo -= PhaseTwoDance;
        foreach (Minion m in minions)
        {
            m.gameObject.SetActive(true);
        }
    }

    void PhaseOneDance()
    {
        state = new OniAttacking(this, 4);
        Instantiate(flag, transform.position + new Vector3(0, 6, 0), Quaternion.identity);
        myHealth.phaseOne -= PhaseOneDance;
        flagSpinner.SetActive(true);
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
        GameObject ogre = GameObject.Find("Ogre");
        if (b)
        {
            ogre.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("MainColor", myHealth.brightColor);
            ogre.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("MainColor", myHealth.brightColor);
        }
        else
        {
            ogre.GetComponent<SkinnedMeshRenderer>().materials[1].SetColor("MainColor", myHealth.brightColor);
            ogre.GetComponent<SkinnedMeshRenderer>().materials[0].SetColor("MainColor", myHealth.brightColor);
        }
    }

    public void TrackPlayer()
    {
        try
        {
            direction = new Vector3(ph.transform.position.x - transform.position.x, 0, ph.transform.position.z - transform.position.z).normalized;
        }
        catch
        {

        }
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
        startFlag.GetComponent<HealthBoss>().deathEvent -= FlagListener;
        state = new OniAttacking(this, 1);
    }

    public void ChangeState(OniInterface newState)
    {
        state = newState;
    }
}
