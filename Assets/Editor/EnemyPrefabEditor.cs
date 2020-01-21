using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPrefab))]
public class EnemyPrefabEditor : BasicCharEditor
{
    private bool nameFold = true;
    private bool StartRace = true;
    private bool healthStats = true;
    private bool bodyStats = true;

    private SerializedProperty NeedFirstName, NeedLastName, RaceList, assingStr,
        assingCharm, assingEnd, assingDex, assingInt, statRngFactor, assingHeight,
        assingFat, assingMuscle;

    private void OnEnable()
    {
        NeedFirstName = serializedObject.FindProperty("NeedFirstName");
        NeedLastName = serializedObject.FindProperty("NeedLastName");
        RaceList = serializedObject.FindProperty("assingRace.Options");
        assingStr = serializedObject.FindProperty("assingStr");
        assingCharm = serializedObject.FindProperty("assingCharm");
        assingEnd = serializedObject.FindProperty("assingEnd");
        assingDex = serializedObject.FindProperty("assingDex");
        assingInt = serializedObject.FindProperty("assingInt");
        statRngFactor = serializedObject.FindProperty("statRngFactor");

        assingHeight = serializedObject.FindProperty("assingHeight");
        assingFat = serializedObject.FindProperty("assingFat");
        assingMuscle = serializedObject.FindProperty("assingMuscle");
    }

    public override void OnInspectorGUI()
    {
        //GUILayout.Label("test");
        EnemyPrefab myTarget = (EnemyPrefab)target;
        nameFold = EditorGUILayout.Foldout(nameFold, "Name", true, EditorStyles.foldout);
        if (nameFold)
        {
            GUILayout.BeginVertical("Box");
            TwoBoldLabels("First name", "Last name");
            GUILayout.BeginHorizontal();
            Identity identity = myTarget.Identity;
            identity.FirstName = EditorGUILayout.TextArea(identity.FirstName);
            identity.LastName = EditorGUILayout.TextArea(identity.LastName);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Give first name");
            serializedObject.Update();
            NeedFirstName.boolValue = EditorGUILayout.Toggle(NeedFirstName.boolValue);
            GUILayout.Label("Give last name");
            NeedLastName.boolValue = EditorGUILayout.Toggle(NeedLastName.boolValue);
            serializedObject.ApplyModifiedProperties();
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
            TwoBoldLabels("Race", "Probability");
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
            TwoBoldLabels("Assing str", "Assing charm");
            EditorGUILayout.BeginHorizontal();
            assingStr.intValue = EditorGUILayout.IntSlider(assingStr.intValue, 0, 99);
            assingCharm.intValue = EditorGUILayout.IntSlider(assingCharm.intValue, 0, 99);
            EditorGUILayout.EndHorizontal();
            TwoBoldLabels("Assing end", "Assing dex");
            EditorGUILayout.BeginHorizontal();
            assingEnd.intValue = EditorGUILayout.IntSlider(assingEnd.intValue, 0, 99);
            assingDex.intValue = EditorGUILayout.IntSlider(assingDex.intValue, 0, 99);
            EditorGUILayout.EndHorizontal();
            TwoBoldLabels("Assing int", "Stats rng factor");
            EditorGUILayout.BeginHorizontal();
            assingInt.intValue = EditorGUILayout.IntSlider(assingInt.intValue, 0, 99);
            statRngFactor.floatValue = EditorGUILayout.Slider(statRngFactor.floatValue, 0, 1f);
            // var  = serializedObject.FindProperty("");
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        bodyStats = EditorGUILayout.Foldout(bodyStats, "Stats", true, EditorStyles.foldout);
        if (bodyStats)
        {
            EditorGUILayout.BeginVertical("Box");
            TwoBoldLabels("Height", "Fat");
            EditorGUILayout.BeginHorizontal();
            assingHeight.intValue = EditorGUILayout.IntSlider(assingHeight.intValue, 0, 500);
            assingFat.intValue = EditorGUILayout.IntSlider(assingFat.intValue, 0, 200);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Label("Standard editor and end of custom editor", EditorStyles.boldLabel);
        GUILayout.Space(20);
        base.OnInspectorGUI();
    }

    private void TwoBoldLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(secondLabel, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
    }
}