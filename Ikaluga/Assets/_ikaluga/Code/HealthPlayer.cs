using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class HealthPlayer : Health
{
    public Material playerMat;
    public Material playerCapeMat;
    public GameObject damageEffect;
    public GameObject shieldEffect;

    private void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (bright)
        {
            Color c = playerMat.GetColor("Emission_Color");
            Color targetColor = brightColor;

            Vector3 v3Color = new Vector3(c.r, c.g, c.b);
            Vector3 targetV3 = new Vector3(targetColor.r, targetColor.g, targetColor.b);

            Vector3 lerp = Vector3.Lerp(v3Color, targetV3, Time.deltaTime * 2f);

            playerCapeMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
            playerMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
        }
        else
        {
            Color c = playerMat.GetColor("Emission_Color");
            Color targetColor = darkColor;

            Vector3 v3Color = new Vector3(c.r, c.g, c.b);
            Vector3 targetV3 = new Vector3(targetColor.r, targetColor.g, targetColor.b);

            Vector3 lerp = Vector3.Lerp(v3Color, targetV3, Time.deltaTime * 2f);

            playerCapeMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
            playerMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
        }

        ParticleSystemRenderer pr = shieldEffect.GetComponent<ParticleSystemRenderer>();
        float a = shieldEffect.GetComponent<ParticleSystemRenderer>().material.GetFloat("Laser_Particle_Alpha");
        if (a > 0)
        {
            a -= Time.deltaTime * 5f;
            pr.material.SetFloat("Laser_Particle_Alpha", a);
        }
    }

    public override void DealDamage(float value)
    {
        AudioSource aS = this.GetComponent<AudioSource>();
        SoundEffects se = FindObjectOfType<SoundEffects>();
        aS.pitch = 1;
        if (value > 0)
        {
            aS.volume = 1f;
            aS.PlayOneShot(se.GetClip("PlayerDamageSound"));

            playerCapeMat.SetColor("Emission_Color", Color.red);
            playerMat.SetColor("Emission_Color", Color.red);
            damageEffect.SetActive(true);
            currentHealth -= value;
        }
        else
        {
            aS.volume = 0.1f;
            aS.PlayOneShot(se.GetClip("PlayerShieldSound"));
            ParticleSystemRenderer pr = shieldEffect.GetComponent<ParticleSystemRenderer>();
            pr.material.SetFloat("Laser_Particle_Alpha", 1f);
            shieldEffect.SetActive(true);
        }

        Debug.Log(this.name + " Current Health = " + currentHealth); 
        if (currentHealth <= 0)
            Die();
    }

    public void QuickChangeColor()
    {
        if (bright)
        {
            playerCapeMat.SetColor("Emission_Color", brightColor);
            playerMat.SetColor("Emission_Color", brightColor);
        }
        else
        {
            playerCapeMat.SetColor("Emission_Color", darkColor);
            playerMat.SetColor("Emission_Color", darkColor);
        }
    }
}
