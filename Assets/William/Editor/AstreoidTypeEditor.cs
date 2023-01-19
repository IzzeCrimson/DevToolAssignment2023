using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(AsteroidType))]
public class AstreoidTypeEditor : Editor
{
    public VisualTreeAsset m_UXML;

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();
        if (m_UXML != null)
        {
            m_UXML.CloneTree(root);
        }

        var foldout = new Foldout()
        {
            viewDataKey = "AstreoidTypeFullInspectorFoldout",
            text = "Orginal Inspector"
        };

        InspectorElement.FillDefaultInspector(foldout, serializedObject, this);
        root.Add(foldout);
        return root;
        
    }


}
