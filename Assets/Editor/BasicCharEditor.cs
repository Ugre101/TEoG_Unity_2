using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ThePrey))]
public class BasicCharEditor : Editor
{
    private ThePrey basicChar;

    public override void OnInspectorGUI()
    {
        basicChar = (ThePrey)target;
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(basicChar.FullName);
        GUILayout.BeginHorizontal();
        GUILayout.Label(basicChar.Gender.ToString());
        GUILayout.Label(basicChar.Race);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.BeginHorizontal();
        GUILayout.Label("First name");
        GUILayout.Label("Last name");
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        basicChar.firstName = EditorGUILayout.TextField(basicChar.firstName);
        basicChar.lastName = EditorGUILayout.TextField(basicChar.lastName);
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}