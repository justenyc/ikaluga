using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCenter : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    [Header("Emission Properties")]
    [Range(0,300)][SerializeField] float _emissionMultiplier = 10;
    [ColorUsage(true,true)]
    [SerializeField] Color _emissionColor;
    [SerializeField] float _emissionFadeSpeed = 1;
    [SerializeField] bool _fade = true;

    [Header("Constraints")]
    [SerializeField] float _spectrumThreshold = 0;

    public delegate void listen();
    public event listen Sync;

    float _spectrumAverage;
    float[] _spectrum = new float[1024];

    // Update is called once per frame
    void Update()
    {
        Listen();
    }

    public float GetAverage()
    {
        return _spectrumAverage;
    }

    public Color GetColor()
    {
        return _emissionColor;
    }

    public bool GetFadeBool()
    {
        return _fade;
    }

    public float GetFadeSpeed()
    {
        return _emissionFadeSpeed;
    }

    public float GetMultiplier()
    {
        return _emissionMultiplier;
    }

    void Listen()
    {
        if (_audioSource == null)
            AudioListener.GetSpectrumData(_spectrum, 0, FFTWindow.Rectangular);
        else
            _audioSource.GetSpectrumData(_spectrum, 0, FFTWindow.Rectangular);

        _spectrumAverage = SpectrumAverage(_spectrum);

        if (_spectrumAverage * _emissionMultiplier > _spectrumThreshold && Sync != null)
            Sync();
    }

    float SpectrumAverage(float[] spectrumBlock)
    {
        float average = 0;
        float sum = 0;

        for (int i = 1; i < spectrumBlock.Length - 1; i++)
        {
            sum += spectrumBlock[i];
        }

        average = sum / spectrumBlock.Length;
        return average;
    }
}
