using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    [HideInInspector] public HealthBoss myHealth;
    public Animator anim;
    [HideInInspector] public Rigidbody rb;
    public GameObject oni;
    public Oni boss;
    [HideInInspector] public HealthPlayer ph;

    [Space(10)]
    public Vector3 direction;
    public float moveSpeed;
    public float turnSpeed = 1;
    public float attackTimer = 5f;
    public bool coroutine = false;
    public bool delayUpdate = true;

    // Start is called before the first frame update
    void Start()
    {
        ph = FindObjectOfType<HealthPlayer>();
        myHealth = this.GetComponent<HealthBoss>();

        anim = oni.GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        ChangeFresnelColor(myHealth.bright);
        GameObject.Find("Ogre").GetComponent<SkinnedMeshRenderer>().materials[1].SetFloat("Vector1_cb68193afe724db7996723ccea4e88f6", 1);
        anim.SetInteger("AnimState", 4);
        StartCoroutine(UpdateDelay());
    }

    private void OnEnable()
    {
        boss.GetComponent<HealthBoss>().deathEvent += DieWithBoss;
    }

    IEnumerator UpdateDelay()
    {
        yield return new WaitForSeconds(3f);
        delayUpdate = false;
        anim.SetInteger("AnimState", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!delayUpdate)
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
                Movement();
            }
            else if (attackTimer <= 0)
            {
                int r = Random.Range(1, 4);
                /*if (Vector3.Distance(ph.transform.position, transform.position) < 10f)
                {
                    anim.SetInteger("AnimState", r);
                    StartCoroutine(DelayNextAttack());
                }
                else
                {
                    anim.SetInteger("AnimState", r);
                }*/
                if (!coroutine)
                {
                    myHealth.bright = !myHealth.bright;
                    ChangeFresnelColor(myHealth.bright);
                    StartCoroutine(DelayNextAttack());
                }
                anim.SetInteger("AnimState", 1);
            }
        }
    }

    IEnumerator DelayNextAttack()
    {
        coroutine = true;
        yield return new WaitForSeconds(3f);
        anim.SetInteger("AnimState", 0);
        attackTimer = 5f;
        coroutine = false;
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

    void Movement()
    {
        FacePlayer();
        rb.position += transform.forward * moveSpeed * Time.deltaTime;
        anim.SetFloat("Move", moveSpeed);
    }

    private void OnDisable()
    {
        boss.GetComponent<HealthBoss>().deathEvent -= DieWithBoss;
    }

    void DieWithBoss()
    {
        Debug.Log("DieWithBoss()");
        myHealth.Die();
    }
}
