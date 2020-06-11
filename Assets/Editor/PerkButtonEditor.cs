using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(PerkButton))]
public class PerkButtonEditor : Editor
{
    protected PerkButton myTarget;
    private SerializedProperty perkInfo = null;
    private SerializedProperty icon = null;
    
    
    private void OnEnable()
    {
        myTarget = (PerkButton)target;
        perkInfo = serializedObject.FindProperty("perkInfo");
        icon = serializedObject.FindProperty("rune");
    }
    public override void OnInspectorGUI()
    {
        if (perkInfo.objectReferenceValue is PerkInfo perk)
        {
            if (perk.Icon != null)
            {
                myTarget.SetRune(perk.Icon);
            }        
        }
        base.OnInspectorGUI();
    }
}
