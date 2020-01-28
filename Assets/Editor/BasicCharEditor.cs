using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BasicChar))]
public class BasicCharEditor : Editor
{
    private BasicChar basicChar;

    public override void OnInspectorGUI()
    {
        basicChar = (BasicChar)target;
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(basicChar.Identity.FullName);
        GUILayout.BeginHorizontal();
        GUILayout.Label(basicChar.Gender.ToString());
        GUILayout.Label(basicChar.Race(true));
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("First name");
        GUILayout.Label("Last name");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        basicChar.Identity.FirstName = EditorGUILayout.TextField(basicChar.Identity.FirstName);
        basicChar.Identity.LastName = EditorGUILayout.TextField(basicChar.Identity.LastName);
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}