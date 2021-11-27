using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PhaseManager : MonoBehaviour
{
    [Header("Boss Weapon Objects")]
    public List<pillar> listOfPillars = new List<pillar>();
    public List<turret> listOfTurrets = new List<turret>();
    public List<missileaim> listOfMissiles = new List<missileaim>();
    public List<mortar> listOfMortars = new List<mortar>();
    public List<core> listOfCores = new List<core>();

    [SerializeField] GameObject[] lasers;

    [Header("Terrain GameObjects")]
    [SerializeField] GameObject forceField;
    [SerializeField] GameObject coreField;
    [SerializeField] GameObject[] midwalls;
    [SerializeField] GameObject[] terrainObject;

    public bool turretsOn;
    public bool missilesOn;
    public bool mortarsOn;
    public bool lasersOn;

    public GameObject bgm;
    public GameObject ambientBgm;

    public enum Phase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Victory,
    }

    private Phase phase;
    // Start is called before the first frame update
    void Start()
    {
        phase = Phase.Phase1;
        listOfPillars = FindObjectsOfType<pillar>().ToList();
        listOfTurrets = FindObjectsOfType<turret>().ToList();
        listOfMissiles = FindObjectsOfType<missileaim>().ToList();
        listOfMortars = FindObjectsOfType<mortar>().ToList();
        listOfCores = FindObjectsOfType<core>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        switch (phase)
        {
            case Phase.Phase1:

                //phase 1 logic
                Debug.Log("phase 1 shake it out");
                if (listOfPillars.Count == 0)
                {
                    phase = Phase.Phase2;
                }
                break;

            case Phase.Phase2:

                //phase 2 logic
                ambientBgm.SetActive(false);
                bgm.SetActive(true);

                Invoke("DestroyField", 3f);

                turretsOn = true;
                missilesOn = true;

                Debug.Log("phase 2 go for broke");
                if (listOfTurrets.Count == 0)
                {
                    phase = Phase.Phase3;
                }
                break;

            case Phase.Phase3:

                //phase 3 logic

                mortarsOn = true;

                foreach (GameObject go in midwalls)
                {
                    go.SetActive(false);
                }
                Debug.Log("phase 3 lets keep on rockin");
                if (listOfMissiles.Count == 0 && listOfMortars.Count == 0)
                {
                    phase = Phase.Phase4;
                }
                break;

            case Phase.Phase4:

                //phase 4 logic

                Debug.Log("phase 4 idk");
                coreField.SetActive(false);
                foreach (GameObject go in terrainObject)
                {
                    go.SetActive(false);
                }

                Invoke("ActivateLasers", 2.5f);

                if (listOfCores.Count == 0)
                {
                    phase = Phase.Victory;
                }
                break;

            case Phase.Victory:
                //win stuff
                foreach (GameObject go in lasers)
                {
                    go.SetActive(false);
                }
                Debug.Log("winner");
                break;
        }
                //Phase 1
                //Destroy pillars, fight starts proper

                //Phase 2
                //Destroy forcefield
                //Turrets active, missiles active, destroy turrets for phase 3

                //Phase 3
                //Destroy Midwalls
                //Mortars active, missiles active, destroy both for phase 4

                //Phase 4
                //Destroy rest of building/arena
                //Laser cone active, destroy core
    }

    private void StartNextPhase()
    {
        switch (phase)
        {
            case Phase.Phase1:
                phase = Phase.Phase2;
                break;
            case Phase.Phase2:
                phase = Phase.Phase3;
                break;
            case Phase.Phase3:
                phase = Phase.Phase4;
                break;
            case Phase.Phase4:
                phase = Phase.Victory;
                break;
        }
    }

    private void DestroyField()
    {
        forceField.SetActive(false);
    }

    private void ActivateLasers()
    {

        foreach (GameObject go in lasers)
        {
            go.SetActive(true);
        }
    }
}
