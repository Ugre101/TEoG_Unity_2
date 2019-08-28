using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(EnemyPrefab))]
public class AssingRaceEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnemyPrefab myTarget = (EnemyPrefab)target;
        if (GUILayout.Button("Add options"))
        {
            myTarget.assingRace.AddOption();
        }
    }
}
