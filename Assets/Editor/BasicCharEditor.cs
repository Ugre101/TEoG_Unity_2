using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BasicChar))]
public class BasicCharEditor : Editor
{
 
    private BasicChar basicChar;
    private void OnEnable() => BasicCharEnable();

    protected void BasicCharEnable() => basicChar = (BasicChar)target;

    public override void OnInspectorGUI()
    {
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(basicChar.Identity.FullName);
        UgreEditorTools.TwoLabels(basicChar.GetGender().ToString(), basicChar.Race(true));
        GUILayout.EndVertical();
        UgreEditorTools.TwoBoldLabels("First name", "Last name");
        GUILayout.BeginHorizontal();
        basicChar.Identity.FirstName = EditorGUILayout.TextField(basicChar.Identity.FirstName);
        basicChar.Identity.LastName = EditorGUILayout.TextField(basicChar.Identity.LastName);
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}