using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(HealthBoss))]
[CanEditMultipleObjects]
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
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(hb);
        }
    }
}