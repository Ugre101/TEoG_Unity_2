using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CharHolder))]
public class CharHolderEditor : Editor
{
    private CharHolder charHolder;
    private BasicChar BasicChar => charHolder.BasicChar;

    public override void OnInspectorGUI()
    {
        charHolder = (CharHolder)target;
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(BasicChar.Identity.FullName);
        UgreEditorTools.TwoLabels(BasicChar.GetGender().ToString(), BasicChar.Race(true));
        GUILayout.EndVertical();
        UgreEditorTools.TwoBoldLabels("First name", "Last name");
        GUILayout.BeginHorizontal();
        BasicChar.Identity.FirstName = EditorGUILayout.TextField(BasicChar.Identity.FirstName);
        BasicChar.Identity.LastName = EditorGUILayout.TextField(BasicChar.Identity.LastName);
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}