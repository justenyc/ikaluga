using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class HealthBoss : Health
{
    public RendererType rendererType;

    public Material mat;

    [SerializeField] [HideInInspector] MeshRenderer meshRenderer;
    [SerializeField] [HideInInspector] SkinnedMeshRenderer skinnedMeshRenderer;
    AudioSource audioSource;
    public delegate void deathDelagate();
    public event deathDelagate deathEvent;

    public delegate void phaseOneDelegate();
    public event phaseOneDelegate phaseOne;

    public delegate void phaseTwoDelegate();
    public event phaseTwoDelegate phaseTwo;

    private void Start()
    {
        base.Start();
        FindMatOnRenderer();
        audioSource = GetComponent<AudioSource>();
    }

    void FindMatOnRenderer()
    {
        switch(rendererType)
        {
            case RendererType.MeshRenderer:
                foreach (Material m in meshRenderer.materials)
                {
                    if (m.name == "Fresnel_Effect (Instance)")
                    {
                        mat = m;
                        return;
                    }
                }
                break;

            case RendererType.SkinnedMeshRenderer:
                foreach (Material m in skinnedMeshRenderer.materials)
                {
                    if (m.name == "Fresnel_Effect (Instance)")
                    {
                        mat = m;
                        return;
                    }
                }
                break;
        }
    }

    private void Update()
    {
        try
        {
            if (bright)
            {
                Color c = mat.GetColor("MainColor");
                Color targetColor = brightColor;

                Vector3 v3Color = new Vector3(c.r, c.g, c.b);
                Vector3 targetV3 = new Vector3(targetColor.r, targetColor.g, targetColor.b);

                Vector3 lerp = Vector3.Lerp(v3Color, targetV3, Time.deltaTime * 2f);

                mat.SetColor("MainColor", new Color(lerp.x, lerp.y, lerp.z));
            }
            else
            {
                Color c = mat.GetColor("MainColor");
                Color targetColor = darkColor;

                Vector3 v3Color = new Vector3(c.r, c.g, c.b);
                Vector3 targetV3 = new Vector3(targetColor.r, targetColor.g, targetColor.b);

                Vector3 lerp = Vector3.Lerp(v3Color, targetV3, Time.deltaTime * 2f);

                mat.SetColor("MainColor", new Color(lerp.x, lerp.y, lerp.z));
            }
        }
        catch
        {

        }

        /*ParticleSystemRenderer pr = shieldEffect.GetComponent<ParticleSystemRenderer>();
        float a = shieldEffect.GetComponent<ParticleSystemRenderer>().material.GetFloat("Laser_Particle_Alpha");
        if (a > 0)
        {
            a -= Time.deltaTime * 5f;
            pr.material.SetFloat("Laser_Particle_Alpha", a);
        }*/
    }

    public override void DealDamage(float value)
    {
        if (value > 0)
        {
            ChangeFresnelColor(Color.red);
            audioSource.volume = 0.15f;
            audioSource.pitch = .9f;
            audioSource.PlayOneShot(GetComponent<SoundEffects>().GetClip("Damage"));
        }
        else
        {
            ChangeFresnelColor(Color.white);
            audioSource.volume = 0.15f;
            audioSource.pitch = 1.1f;
            audioSource.PlayOneShot(GetComponent<SoundEffects>().GetClip("Shield"));
        }
        base.DealDamage(value);
        
        if (currentHealth <= maxHealth * 0.6f)
        {
            if (phaseOne != null)
                phaseOne();
        }

        if (currentHealth <= maxHealth * 0.3f)
        {
            if (phaseTwo != null)
                phaseTwo();
        }
    }

    public override void Die()
    {
        if (meshRenderer != null)
            meshRenderer.enabled = false;
        else
            skinnedMeshRenderer.enabled = false;

        GetComponent<Collider>().enabled = false;

        if(deathEvent != null)
        {
            deathEvent();
        }
        Destroy(this.gameObject, .35f);
    }

    void ChangeFresnelColor(Color color)
    {
        mat.SetColor("MainColor", color);
    }

    public MeshRenderer GetMeshRenderer()
    {
        return meshRenderer;
    }

    public void SetMeshRenderer(MeshRenderer mr)
    {
        meshRenderer = mr;
    }

    public SkinnedMeshRenderer GetSkinnedMeshRenderer()
    {
        return skinnedMeshRenderer;
    }

    public void SetSkinnedMeshRenderer(SkinnedMeshRenderer mr)
    {
        skinnedMeshRenderer = mr;
    }
}

public enum RendererType
{
    MeshRenderer,
    SkinnedMeshRenderer
};