using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class HealthBoss : Health
{
    public RendererType rendererType;

    Material mat;

    [SerializeField] [HideInInspector] MeshRenderer meshRenderer;
    [SerializeField] [HideInInspector] SkinnedMeshRenderer skinnedMeshRenderer;

    private void Start()
    {
        base.Start();
        FindMatOnRenderer();
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
                        Debug.Log("Found Mat");
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
                        Debug.Log("Found Mat");
                        return;
                    }
                }
                break;
        }
    }

    private void Update()
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
        base.DealDamage(value);
        if (value > 0)
        {
            currentHealth -= value;
            ChangeFresnelColor(Color.red);
        }
        else
        {
            ChangeFresnelColor(Color.white);
        }
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
 
[CustomEditor(typeof(HealthBoss))]
public class MyScriptEditor : Editor
{
    override public void OnInspectorGUI()
    {
        HealthBoss hb = (HealthBoss)target;

        DrawDefaultInspector();

        if (hb.rendererType == RendererType.MeshRenderer)
        {
            hb.SetMeshRenderer((MeshRenderer)EditorGUILayout.ObjectField("Mesh Renderer", hb.GetMeshRenderer(), typeof(MeshRenderer), true));
        }
        else
        {
            hb.SetSkinnedMeshRenderer((SkinnedMeshRenderer)EditorGUILayout.ObjectField("Skinned Mesh Renderer", hb.GetSkinnedMeshRenderer(), typeof(SkinnedMeshRenderer), true));
        }
    }
}