#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(tileScriptableObject))]
public class tileScriptableObjectEditor : Editor
{
    private tileScriptableObject tileSO;

    private void OnEnable()
    {
        tileSO = target as tileScriptableObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (tileSO.tileImage == null)
        {
            return;
        }

        Texture2D sprite = AssetPreview.GetAssetPreview(tileSO.tileImage);

        GUILayout.Label("", GUILayout.Height(360), GUILayout.Width(360));

        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), sprite);
    }
}
#endif