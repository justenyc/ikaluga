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

    ThirdPersonController tpc;

    private void Start()
    {
        base.Start();
        tpc = this.GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (bright)
        {
            Color c = playerMat.GetColor("Emission_Color");
            Color targetColor = tpc.brightColor;

            Vector3 v3Color = new Vector3(c.r, c.g, c.b);
            Vector3 targetV3 = new Vector3(targetColor.r, targetColor.g, targetColor.b);

            Vector3 lerp = Vector3.Lerp(v3Color, targetV3, Time.deltaTime * 2f);

            playerCapeMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
            playerMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
        }
        else
        {
            Color c = playerMat.GetColor("Emission_Color");
            Color targetColor = tpc.darkColor;

            Vector3 v3Color = new Vector3(c.r, c.g, c.b);
            Vector3 targetV3 = new Vector3(targetColor.r, targetColor.g, targetColor.b);

            Vector3 lerp = Vector3.Lerp(v3Color, targetV3, Time.deltaTime * 2f);

            playerCapeMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
            playerMat.SetColor("Emission_Color", new Color(lerp.x, lerp.y, lerp.z));
        }
    }

    public override void DealDamage(float value)
    {
        AudioSource aS = this.GetComponent<AudioSource>();
        SoundEffects se = FindObjectOfType<SoundEffects>();
        aS.volume = 1f;
        aS.PlayOneShot(se.GetClip("PlayerDamageSound"));

        playerCapeMat.SetColor("Emission_Color", Color.red);
        playerMat.SetColor("Emission_Color", Color.red);
        damageEffect.SetActive(true);
        currentHealth -= value;
        Debug.Log(this.name + " Current Health = " + currentHealth);
        if (currentHealth <= 0)
            Die();
    }
}
