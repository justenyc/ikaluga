using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniAttacks : MonoBehaviour
{
    public GameObject swirl;
    public GameObject sphere;
    public GameObject cascade;
    public GameObject rotatingCascade;
    public Transform bulletSpawnPoint;

    public void Attack(string s)
    {
        switch (s)
        {
            case "Cascade":
                Instantiate(cascade, bulletSpawnPoint.position, Quaternion.Euler(new Vector3(0,180,0)));
                break;

            case "RotatingCascade":
                Instantiate(rotatingCascade, bulletSpawnPoint.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                break;

            case "Sphere":
                Instantiate(sphere, bulletSpawnPoint.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                break;

            case "Swirl":
                Instantiate(swirl, bulletSpawnPoint.position, Quaternion.Euler(new Vector3(0, 180, 0)));
                break;

            default:
                Debug.Log("No string entered");
                break;
        }
    }
}
