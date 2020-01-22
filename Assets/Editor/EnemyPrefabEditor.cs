using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPrefab))]
public class EnemyPrefabEditor : BasicCharEditor
{
    private bool nameFold = true;
    private bool StartRace = true;
    private bool healthStats = true;
    private bool bodyStats = true;

    private SerializedProperty NeedFirstName, NeedLastName;
    private SerializedProperty startRaces;
    private SerializedProperty RaceList, assingStr;
    private SerializedProperty assingCharm, assingEnd, assingDex, assingInt, statRngFactor;
    private SerializedProperty assingHeight, heightRng, assingFat, fatRng, assingMuscle, muscleRng;

    private void OnEnable()
    {
        NeedFirstName = serializedObject.FindProperty("NeedFirstName");
        NeedLastName = serializedObject.FindProperty("NeedLastName");

        startRaces = serializedObject.FindProperty("startRaces");
        RaceList = serializedObject.FindProperty("assingRace.Options");

        assingStr = serializedObject.FindProperty("assingStr");
        assingCharm = serializedObject.FindProperty("assingCharm");
        assingEnd = serializedObject.FindProperty("assingEnd");
        assingDex = serializedObject.FindProperty("assingDex");
        assingInt = serializedObject.FindProperty("assingInt");
        statRngFactor = serializedObject.FindProperty("statRngFactor");

        assingHeight = serializedObject.FindProperty("assingHeight");
        heightRng = serializedObject.FindProperty("heightRng");
        assingFat = serializedObject.FindProperty("assingFat");
        fatRng = serializedObject.FindProperty("fatRng");
        assingMuscle = serializedObject.FindProperty("assingMuscle");
        muscleRng = serializedObject.FindProperty("muscleRng");
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
            serializedObject.Update();
            startRaces.arraySize = EditorGUILayout.IntField("Size", startRaces.arraySize);
            if (GUILayout.Button("Add start race"))
            {
                startRaces.arraySize++;
            }
            TwoBoldLabels("Race", "Probability");
            for (int i = 0; i < startRaces.arraySize; i++)
            {
                var race = startRaces.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(race, new GUIContent("Race " + i), true);
            }
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
        }
        healthStats = EditorGUILayout.Foldout(healthStats, "Stats", true, EditorStyles.foldout);
        if (healthStats)
        {
            EditorGUILayout.BeginVertical("Box");
            serializedObject.Update();
            TwoBoldLabels("Assing str", "Assing charm");
            TwoIntSliders(assingStr, assingCharm);
            TwoBoldLabels("Assing end", "Assing dex");
            TwoIntSliders(assingEnd, assingDex);
            TwoBoldLabels("Assing int", "Stats rng factor");
            IntAndFloatSlider(assingInt, statRngFactor);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        bodyStats = EditorGUILayout.Foldout(bodyStats, "Stats", true, EditorStyles.foldout);
        if (bodyStats)
        {
            EditorGUILayout.BeginVertical("Box");
            serializedObject.Update();
            TwoBoldLabels("Height", "Height rng");
            IntAndFloatSlider(assingHeight, heightRng);
            TwoBoldLabels("Fat", "Fat rng");
            IntAndFloatSlider(assingFat, fatRng);
            TwoBoldLabels("Muscle", "Muscle rng");
            IntAndFloatSlider(assingMuscle, muscleRng);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.Label("Standard editor and end of custom editor", EditorStyles.boldLabel);
        GUILayout.Space(20);
        base.OnInspectorGUI();
    }

    private void IntAndFloatSlider(SerializedProperty intSlider, SerializedProperty floatSlider)
    {
        EditorGUILayout.BeginHorizontal();
        intSlider.intValue = EditorGUILayout.IntSlider(intSlider.intValue, 0, 500);
        floatSlider.floatValue = EditorGUILayout.Slider(floatSlider.floatValue, 0f, 1f);
        EditorGUILayout.EndHorizontal();
    }

    private void TwoIntSliders(SerializedProperty one, SerializedProperty two, int maxVal = 99)
    {
        EditorGUILayout.BeginHorizontal();
        one.intValue = EditorGUILayout.IntSlider(one.intValue, 0, maxVal);
        two.intValue = EditorGUILayout.IntSlider(two.intValue, 0, maxVal);
        EditorGUILayout.EndHorizontal();
    }

    private void TwoBoldLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(secondLabel, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
    }
}