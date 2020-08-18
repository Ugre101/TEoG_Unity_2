using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssestHandler
{
    [OnOpenAsset()]
    public static bool OpenEditor(int instanceId, int line)
    {
        object obj = EditorUtility.InstanceIDToObject(instanceId);
        if (obj is GameObject gameObject)
        { /*
            if (gameObject.GetComponent<EnemyHolder>() is EnemyHolder enemyHolder)
            {
                EnemyEditorWindow.Open(enemyHolder);
                return true;
            }
            else if (gameObject.GetComponent<CharHolder>() is CharHolder holder)
            {
                BasicCharEditorWindow.Open(holder);
                return true;
            }*/
        }
        return false;
    }
}

[CustomEditor(typeof(CharHolder))]
public class CharHolderEditor : Editor
{
    private CharHolder charHolder;
    private BasicChar BasicChar => charHolder.BasicChar;

    public override void OnInspectorGUI()
    {
        charHolder = (CharHolder)target;
        /*
        if (GUILayout.Button("Open window"))
        {
            BasicCharEditorWindow.Open(charHolder);
        }
        */
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(BasicChar.Identity.FullName);
        UgreEditorTools.TwoLabels(BasicChar.Gender().ToString(), BasicChar.Race(true));
        GUILayout.EndVertical();
        UgreEditorTools.TwoBoldLabels("First name", "Last name");
        GUILayout.BeginHorizontal();
        BasicChar.Identity.SetFirstName(EditorGUILayout.TextField(BasicChar.Identity.FirstName));
        BasicChar.Identity.SetLastName(EditorGUILayout.TextField(BasicChar.Identity.LastName));
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Id", EditorStyles.boldLabel);
        EditorGUILayout.LabelField(BasicChar.Identity.Id);
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}