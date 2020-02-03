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
        UgreEditorTools.TwoBoldLabels("First name", "Last name");
        GUILayout.BeginHorizontal();
        basicChar.Identity.FirstName = EditorGUILayout.TextField(basicChar.Identity.FirstName);
        basicChar.Identity.LastName = EditorGUILayout.TextField(basicChar.Identity.LastName);
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}