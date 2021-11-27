using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{

    public AudioSource audioSource;
    public float updateStep = 0.1f;
    public int sampleDataLength = 0; //1024;

    private float currentUpdateTime = 0f;

    private float clipLoudness;
    private float[] clipSampleData;

    private Vector3 originalScale;

    public float yoffset = 10.0f;

    //reduced to 512 sampling the max is a bit much ;)
    public int numOfSamples = 512; //Min: 64, Max: 8192

    //Removed Updated to AudioListener
    //public AudioSource aSource;

    public GameObject[] g;

    public float[] freqData;
    public float[] band;
    public float[] previous_bands;

    public GameObject projectile;

    private float most_recent_processed_beat = 0f;
    private float most_recent_processed_playback_time = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (!audioSource)
        {
            Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
        }
        clipSampleData = new float[sampleDataLength];

        originalScale = transform.localScale;
        Debug.Log("Original Scale");
        Debug.Log(originalScale);







        freqData = new float[numOfSamples];

        int n = freqData.Length;

        if ((numOfSamples & (numOfSamples - 1)) != 0)
        {
            Debug.LogError("FreqData length " + numOfSamples + " is not a power of 2!!! Min: 64, Max: 8192.");
            return;
        }

        int k = 0;
        for (int j = 0; j < freqData.Length; j++)
        {
            n = n / 2;
            if (n <= 0) break;
            k++;
        }

        band = new float[k + 1];
        previous_bands = new float[k + 1];
        g = new GameObject[k + 1];


        for (int i = 0; i < band.Length; i++)
        {
            band[i] = 0;
            g[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g[i].GetComponent<Renderer>().material.SetColor("_Color", Color.cyan);
            g[i].transform.position = new Vector3(i, yoffset, 30);

        }

        InvokeRepeating("check", 0.0f, 1.0f / 15.0f); // update at 15 fps

    }

    // Use this for initialization
    void Awake()
    {



    }

    // Update is called once per frame
    void Update()
    {

        float scaling_multiplier;
        Vector3 newScale;

        //currentUpdateTime += Time.deltaTime;
        //if (currentUpdateTime >= updateStep)
        //{
        currentUpdateTime = 0f;
        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //I read 1024 samples, which is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
        clipLoudness = 0f;
        //Debug.Log("Clip Sample Data");
        foreach (var sample in clipSampleData)
        {
            //Debug.Log(clipSampleData);

            clipLoudness += Mathf.Abs(sample);
        }
        clipLoudness /= sampleDataLength; //clipLoudness is what you are looking for
        //}

        scaling_multiplier = Mathf.Abs(clipLoudness) * 10.0f;

        //Debug.Log("scaling_multiplier");
        //Debug.Log(scaling_multiplier);

        newScale = new Vector3(originalScale.x * scaling_multiplier, originalScale.y, originalScale.z * scaling_multiplier);
        //Debug.Log("newScale");
        //Debug.Log(newScale);

        transform.localScale = newScale;

        Debug.Log("Playback Time:");
        Debug.Log(audioSource.time);
        MusicEvent(audioSource.time);
    }


    private void check()
    {
        //Updated to Audio Listener this removes the tie to your audio source
        //Have one Audio Source in the scene playing, this allows multiple Listeners
        //to process the same Audio Source in a scene without tying up resources for
        //Source playing multiple sounds
        AudioListener.GetSpectrumData(freqData, 0, FFTWindow.Rectangular);

        int k = 0;
        int crossover = 2;

        for (int i = 0; i < freqData.Length; i++)
        {
            float d = freqData[i];
            float b = band[k];

            // find the max as the peak value in that frequency band.
            band[k] = (d > b) ? d : b;

            if (i > (crossover - 3))
            {
                float delta;
                k++;
                crossover *= 2;   // frequency crossover point for each band.
                Vector3 tmp = new Vector3(g[k].transform.position.x, yoffset + (band[k] * 32), g[k].transform.position.z);
                delta = band[k] - previous_bands[k];
                if (k == 1 && band[k]*100 > 0.8)
                {
                    //Debug.Log("Threshold exceeded");
                    //GameObject newBullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z) + Camera.main.transform.forward * 1, Camera.main.transform.rotation);
                    //Projectile p = newBullet.GetComponent<Projectile>();
                    //p.ChangeColor(health.bright);
                    //p.ChangeDamage(playerDamage);
                    //p.originObject = this.gameObject;
                }
                if (k == 2 && band[k] * 100 > 0.8)
                {
                    //Debug.Log("Threshold exceeded");
                    //GameObject newBullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 5.5f, transform.position.z) + Camera.main.transform.forward * 1, Camera.main.transform.rotation);
                    //Projectile p = newBullet.GetComponent<Projectile>();
                    //p.ChangeColor(health.bright);
                    //p.ChangeDamage(playerDamage);
                    //p.originObject = this.gameObject;
                }
                if (k == 3 && band[k] * 100 > 0.8)
                {
                    //Debug.Log("Threshold exceeded");
                    //GameObject newBullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 7.5f, transform.position.z) + Camera.main.transform.forward * 1, Camera.main.transform.rotation);
                    //Projectile p = newBullet.GetComponent<Projectile>();
                    //p.ChangeColor(health.bright);
                    //p.ChangeDamage(playerDamage);
                    //p.originObject = this.gameObject;
                }
                if (k == 4 && band[k] * 100 > 0.8)
                {
                    //Debug.Log("Threshold exceeded");
                    //GameObject newBullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 9.5f, transform.position.z) + Camera.main.transform.forward * 1, Camera.main.transform.rotation);
                    //Projectile p = newBullet.GetComponent<Projectile>();
                    //p.ChangeColor(health.bright);
                    //p.ChangeDamage(playerDamage);
                    //p.originObject = this.gameObject;
                }
                band[k] = 0;
                g[k].transform.position = tmp;
                previous_bands[k] = band[k];
            }
        }
        //Debug.Log("Band:");
        //for (int i = 0; i < band.Length; i++)
        //{
        //    Debug.Log(band[i]);
        //    band[i] = 0;
        //}
    }

    private void MusicEvent(float given_time)
    {
        //No work needed, we are seeing a duplicate playback time
        if (most_recent_processed_playback_time == given_time)
        {
            return;
        }

        int bpm = 75;

        float tolerance = 0.01f;
        float onbeat = given_time / bpm;

        float beatnumber_remainder= (given_time % onbeat);
        float beatnumber = given_time / onbeat;

        Debug.Log("beatnumber");
        Debug.Log(beatnumber); 

        if ((beatnumber < tolerance) && (beatnumber > most_recent_processed_beat))
        {
            
            //GameObject newBullet = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 9.5f, transform.position.z) + Camera.main.transform.forward * 1, Camera.main.transform.rotation);
            //Projectile p = newBullet.GetComponent<Projectile>();
        }

        most_recent_processed_beat = beatnumber;
        most_recent_processed_playback_time = given_time;
    }
}

