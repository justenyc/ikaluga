using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniAttacks : MonoBehaviour
{
    public GameObject swirl;
    public GameObject sphere;
    public GameObject cascade;
    public GameObject rotatingCascade;
    public GameObject shockwave;
    public Transform bulletSpawnPoint;
    public Oni manager;

    public void Attack(string s)
    {
        switch (s)
        {
            case "Cascade":
                GameObject c = Instantiate(cascade, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0, 90, 0)));
                Cascade[] cascades = c.GetComponentsInChildren<Cascade>();
                foreach (Cascade ca in cascades)
                {
                    ca.SetProjColor(this.GetComponentInParent<HealthBoss>());
                }
                break;

            case "RotatingCascade":
                GameObject rc = Instantiate(rotatingCascade, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0, 90, 0)));
                Cascade[] rotatingCascades = rc.GetComponentsInChildren<Cascade>();
                foreach (Cascade ca in rotatingCascades)
                {
                    ca.SetProjColor(this.GetComponentInParent<HealthBoss>());
                }
                break;

            case "Sphere":
                GameObject sp = Instantiate(sphere, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0, 90, 0)));
                sp.GetComponent<Sphere>().SetProjColor(this.GetComponentInParent<HealthBoss>());
                break;

            case "Swirl":
                GameObject sw = Instantiate(swirl, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0,90,0)));
                sw.GetComponent<Swirl>().SetProjColor(this.GetComponentInParent<HealthBoss>());
                break;

            case "Shockwave":
                GameObject sh = Instantiate(shockwave, bulletSpawnPoint.position, Quaternion.identity);
                sh.GetComponent<Shockwave>().ChangeColor(this.GetComponentInParent<HealthBoss>().bright);
                break;

            default:
                Debug.Log("No string entered");
                break;
        }
    }

    public void ResetAnimState()
    {
        try
        {
            manager.anim.SetInteger("AnimState", 0);
        }
        catch
        {

        }
    }
}
