using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncListener : MonoBehaviour
{
    SyncCenter syncCenter;
    [SerializeField] bool _override;
    [Header("Emission Properties")]
    [Range(0,300)] [SerializeField] float _emissionMultiplier = 10;
    [ColorUsage(true,true)]
    [SerializeField] Color _emissionColor;
    [SerializeField] float _emissionFadeSpeed = 1;
    [SerializeField] bool _fade = false;
    
    [Header("Constraints")]
    [SerializeField] float _spectrumThreshold = 0;
    [SerializeField] Advanced advancedOptions;

    float _spectrumAverage;

    MeshRenderer mr;
    ParticleSystemRenderer psr;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            syncCenter = FindObjectOfType<SyncCenter>();
            syncCenter.Sync += Update;
            CheckOverride();
        }
        catch
        {
            Debug.LogError("Sync Center not found");
            Debug.Break();
        }

        if (advancedOptions.emissionReference == "" || advancedOptions.emissionReference == null)
            advancedOptions.emissionReference = "_EmissionColor";

        if (mr = this.GetComponent<MeshRenderer>())
        {
            mr = this.GetComponent<MeshRenderer>();
            mr.materials[advancedOptions.index].EnableKeyword(advancedOptions.emissionReference);
        }

        if (psr = this.GetComponent<ParticleSystemRenderer>())
        {
            psr = this.GetComponent<ParticleSystemRenderer>();
            psr.materials[advancedOptions.index].EnableKeyword(advancedOptions.emissionReference);
        }
        //if (mr.materials.Length > 1)
        //advancedOptions.index = SearchForEmissiveMaterial(mr.materials);
    }

    // Update is called once per frame
    void Update()
    {
        if (psr != null || mr != null)
        {
            CheckOverride();
            LerpEmissionDown();
            EmissionBeat();
        }
    }

    void CheckOverride()
    {
        if (_override == false)
        {
            _emissionMultiplier = syncCenter.GetMultiplier();
            _emissionColor = syncCenter.GetColor();
            _emissionFadeSpeed = syncCenter.GetFadeSpeed();
            _fade = syncCenter.GetFadeBool();
        }
    }

    void LerpEmissionDown()
    {
        if (_fade)
        {
            if (mr != null)
            {
                Color currentColor = mr.materials[advancedOptions.index].GetColor(advancedOptions.emissionReference);
                Vector3 vector3Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                vector3Color = Vector3.Lerp(vector3Color, new Vector3(currentColor.r, currentColor.g, currentColor.b) * 0.5f, Time.deltaTime * _emissionFadeSpeed);
                mr.materials[advancedOptions.index].SetColor(advancedOptions.emissionReference, new Color(vector3Color.x, vector3Color.y, vector3Color.z));
            }
            if (psr != null)
            {
                Color currentColor = psr.materials[advancedOptions.index].GetColor(advancedOptions.emissionReference);
                Vector3 vector3Color = new Vector3(currentColor.r, currentColor.g, currentColor.b);
                vector3Color = Vector3.Lerp(vector3Color, new Vector3(currentColor.r, currentColor.g, currentColor.b) * 0.5f, Time.deltaTime * _emissionFadeSpeed);
                psr.materials[advancedOptions.index].SetColor(advancedOptions.emissionReference, new Color(vector3Color.x, vector3Color.y, vector3Color.z));
            }
        }
    }

    void EmissionBeat()
    {
        _spectrumAverage = syncCenter.GetAverage();

        if (mr != null)
            mr.materials[advancedOptions.index].SetColor(advancedOptions.emissionReference, _emissionColor * _spectrumAverage * _emissionMultiplier);

        if (psr != null)
            psr.materials[advancedOptions.index].SetColor(advancedOptions.emissionReference, _emissionColor * _spectrumAverage * _emissionMultiplier);
    }

    int SearchForEmissiveMaterial(Material[] mats)
    {
        //Unused
        for (int ii = 0; ii < mats.Length; ii++)
        {
            if (mats[ii].name.ToLower().Contains("emis"))
            {
                return ii;
            }
        }
        return 0;
    }
}

[System.Serializable]
struct Advanced
{
    public string emissionReference;
    public int index;
}