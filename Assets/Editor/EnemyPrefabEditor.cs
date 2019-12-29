using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPrefab))]
public class EnemyPrefabEditor : BasicCharEditor
{
    private bool nameFold = true;
    private bool StartRace = true;
    private bool healthStats = true;

    public override void OnInspectorGUI()
    {
        //GUILayout.Label("test");
        EnemyPrefab myTarget = (EnemyPrefab)target;
        nameFold = EditorGUILayout.Foldout(nameFold, "Name", true, EditorStyles.foldout);
        if (nameFold)
        {
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("First name");
            GUILayout.Label("Last name");
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            myTarget.Identity.FirstName = EditorGUILayout.TextArea(myTarget.Identity.FirstName);
            myTarget.Identity.LastName = EditorGUILayout.TextArea(myTarget.Identity.LastName);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Give first name");
            myTarget.NeedFirstName = EditorGUILayout.Toggle(myTarget.NeedFirstName);
            GUILayout.Label("Give last name");
            myTarget.NeedLastName = EditorGUILayout.Toggle(myTarget.NeedLastName);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
            GUILayout.EndVertical();
        }
        StartRace = EditorGUILayout.Foldout(StartRace, "Assing race", true, EditorStyles.foldout);
        if (StartRace)
        {
            GUILayout.BeginVertical("Box");
            if (GUILayout.Button("Add options"))
            {
                myTarget.assingRace.AddOption();
            }
            serializedObject.Update();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Race", EditorStyles.boldLabel);
            GUILayout.Label("Probability", EditorStyles.boldLabel);
            GUILayout.EndHorizontal();
            SerializedProperty RaceList = serializedObject.FindProperty("assingRace.Options");
            for (int i = 0; i < RaceList.arraySize; i++)
            {
                EditorGUILayout.PropertyField(RaceList.GetArrayElementAtIndex(i));
            }
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
        }
        healthStats = EditorGUILayout.Foldout(healthStats, "Stats", true, EditorStyles.foldout);
        if (healthStats)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("HP", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("WP", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            serializedObject.Update();
            var hp = serializedObject.FindProperty("assingHP");
            var wp = serializedObject.FindProperty("assingWP");
            hp.intValue = EditorGUILayout.IntSlider(hp.intValue, 1, 9999);
            wp.intValue = EditorGUILayout.IntSlider(wp.intValue, 1, 9999);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Assing str", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Assing charm", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            var assingStr = serializedObject.FindProperty("assingStr");
            assingStr.intValue = EditorGUILayout.IntSlider(assingStr.intValue, 0, 99);
            var assingCharm = serializedObject.FindProperty("assingCharm");
            assingCharm.intValue = EditorGUILayout.IntSlider(assingCharm.intValue, 0, 99);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Assing end", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Assing dex", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            var assingEnd = serializedObject.FindProperty("assingEnd");
            assingEnd.intValue = EditorGUILayout.IntSlider(assingEnd.intValue, 0, 99);
            var assingDex = serializedObject.FindProperty("assingDex");
            assingDex.intValue = EditorGUILayout.IntSlider(assingDex.intValue, 0, 99);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Assing int", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Stats rng factor", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            var assingInt = serializedObject.FindProperty("assingInt");
            assingInt.intValue = EditorGUILayout.IntSlider(assingInt.intValue, 0, 99);
            var statRngFactor = serializedObject.FindProperty("statRngFactor");
            statRngFactor.floatValue = EditorGUILayout.Slider(statRngFactor.floatValue, 0, 1f);
            // var  = serializedObject.FindProperty("");
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        GUILayout.Label("Standard editor and end of custom editor", EditorStyles.boldLabel);
        GUILayout.Space(20);
        base.OnInspectorGUI();
    }
}