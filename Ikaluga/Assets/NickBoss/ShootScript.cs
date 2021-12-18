using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject player;

    public GameObject slowTurret1;
    public GameObject slowTurret2;

    public GameObject MachineGunTurret;

    public GameObject projectileBright;
    public GameObject projectileDark;
    public GameObject fastProjectileBright;
    public GameObject fastProjectileDark;

    private bool alternateProjectileOne = false;
    private bool alternateProjectileTwo = false;

    public GameObject mine;
    private bool mineBright = false;

    public GameObject[] blocking_cubes;
    private int current_invisible_cube = 0;

    //ProjectileWave Values
    public GameObject blackWaveProjectile;
    public GameObject whiteWaveProjectile;
    
    //We want the player to be able to move around the projectiles
    //So we have two different sets of locations the projectiles can spawn
    //This might create a checkerboard pattern with gaps to move between for example
    //One traversal bullet will be spawned per spawn location or offset spawn location array item.
    public Vector3[] spawnLocations;
    public Vector3[] offsetSpawnLocations;
    public Quaternion spawnRotation;
    public float WaveProjectileSpeed = 1;
    private bool alternateWave;

    //Machine Gun variables
    public bool MachineGunEnabled = false;
    public float FireRate = 0.1f;
    public float MaxPauseDuration = 0.2f;
    public float timeSinceLastFired = 0.0f;
    public bool machineGunColor = false;
    public float CurrentPauseDuration = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (MachineGunEnabled)
        {
            timeSinceLastFired = timeSinceLastFired + Time.deltaTime;
            //If current pause duration is > 0 that means we are paused for the moment.
            //If it's 0 or less we can fire away.
            if (CurrentPauseDuration > 0)
            {
                CurrentPauseDuration = CurrentPauseDuration + Time.deltaTime;
                if (CurrentPauseDuration > MaxPauseDuration)
                {
                    CurrentPauseDuration = 0.0f;
                }
            }
            else
            {
                if (timeSinceLastFired > FireRate)
                {
                    shootfastshot();
                    timeSinceLastFired = 0.0f;
                }
            }

        }
    }

    public void EnableMachineGun()
    {
        MachineGunEnabled = true;
        HideAllBlockingCubes();
    }

    public void PauseMachineGun()
    {
        //If current pause duration is > 0 that means we are paused for the moment.
        //So we set it to a very small number that is still > 0
        CurrentPauseDuration = 0.00001f;
        ChangeMachineGunColor();
    }

    public void ChangeMachineGunColor()
    {
        if(machineGunColor)
        {
            machineGunColor = false;
        }
        else
        {
            machineGunColor = true;
        }
    }

    public void shootshotOne()
    {

        //This rotation doesn't do anything because it's immediately overwritten but I'm too lazy to define a different spawn rotation
        var rotationAngle = Quaternion.LookRotation(slowTurret1.transform.position - transform.position);
        if (alternateProjectileOne)
        {
            GameObject newProjectile = Instantiate(projectileBright, slowTurret1.transform.position, rotationAngle);
            newProjectile.transform.LookAt(player.transform);
            alternateProjectileOne = false;
        }
        else
        {
            GameObject newProjectile = Instantiate(projectileDark, slowTurret1.transform.position, rotationAngle);
            newProjectile.transform.LookAt(player.transform);
            alternateProjectileOne = true;
        }
    }

    public void shootshottwo()
    {
        //This rotation doesn't do anything because it's immediately overwritten but I'm too lazy to define a different spawn rotation
        var rotationAngle = Quaternion.LookRotation(slowTurret2.transform.position - transform.position);
        if (alternateProjectileTwo)
        {
            GameObject newProjectile = Instantiate(projectileBright, slowTurret2.transform.position, rotationAngle);
            newProjectile.transform.LookAt(player.transform);
            alternateProjectileTwo = false;
        }
        else
        {
            GameObject newProjectile = Instantiate(projectileDark, slowTurret2.transform.position, rotationAngle);
            newProjectile.transform.LookAt(player.transform);
            alternateProjectileTwo = true;
        }
    }

    public void shootfastshot()
    {
        //This rotation doesn't do anything because it's immediately overwritten but I'm too lazy to define a different spawn rotation
        var rotationAngle = Quaternion.LookRotation(MachineGunTurret.transform.position - transform.position);
        if (machineGunColor)
        {
            GameObject newProjectile = Instantiate(fastProjectileBright, MachineGunTurret.transform.position, rotationAngle);
            newProjectile.transform.LookAt(player.transform);
        }
        else
        {
            GameObject newProjectile = Instantiate(fastProjectileDark, MachineGunTurret.transform.position, rotationAngle);
            newProjectile.transform.LookAt(player.transform);
        }
    }


    public void PlaceMineAtPlayer()
    {
        GameObject newMine = Instantiate(mine, player.transform.position, player.transform.rotation);
        newMine.GetComponent<MineExplodeScript>().ChangeColor(mineBright, null);
        if (mineBright)
        {
            mineBright = false;
        }
        else
        {
            mineBright = true;
        }
        ChangeBlockingCube();
    }

    public void ChangeBlockingCube()
    {
        
        blocking_cubes[current_invisible_cube].SetActive(true);
        current_invisible_cube = current_invisible_cube + 1;

        if (current_invisible_cube >= blocking_cubes.Length)
        {
            current_invisible_cube = 0;
        }

        blocking_cubes[current_invisible_cube].SetActive(false);

    }

    public void HideAllBlockingCubes()
    {
        foreach(GameObject singleCube in blocking_cubes)
        {
            singleCube.SetActive(false);
        }
    }

    public void SpawnProjectileWave()
    {
        if (alternateWave)
        {
            foreach (Vector3 spawnLocation in spawnLocations)
            {
                GameObject newProjectile = Instantiate(whiteWaveProjectile, spawnLocation, spawnRotation);
            }
            alternateWave = false;
        }
        else
        {
            foreach (Vector3 spawnLocation in offsetSpawnLocations)
            {
                GameObject newProjectile = Instantiate(blackWaveProjectile, spawnLocation, spawnRotation);
            }
            alternateWave = true;
        }
    }
}
