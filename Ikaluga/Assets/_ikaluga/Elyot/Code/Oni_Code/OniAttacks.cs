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
                Instantiate(cascade, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0, 90, 0)));
                break;

            case "RotatingCascade":
                Instantiate(rotatingCascade, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0, 90, 0)));
                break;

            case "Sphere":
                Instantiate(sphere, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0, 90, 0)));
                break;

            case "Swirl":
                Instantiate(swirl, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.rotation.eulerAngles - new Vector3(0,90,0)));
                break;

            case "Shockwave":
                Instantiate(shockwave, bulletSpawnPoint.position, Quaternion.identity);
                break;

            default:
                Debug.Log("No string entered");
                break;
        }
    }

    public void ResetAnimState()
    {
        manager.anim.SetInteger("AnimState", 0);
    }
}
