using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyPrefab))]
public class EnemyPrefabEditor : BasicCharEditor
{
    private bool nameFold = true;
    private bool StartRace = true;
    private bool healthStats = true;
    private bool bodyStats = true;
    private bool rewardFold = true;

    private SerializedProperty NeedFirstName, NeedLastName;
    private SerializedProperty startRaces;
    private SerializedProperty assingStr, assingCharm, assingEnd, assingDex, assingInt, statRngFactor;
    private SerializedProperty assingHeight, heightRng, assingFat, fatRng, assingMuscle, muscleRng;
    private SerializedProperty reward, rewardExp, rewardGold, rewardRng, drops;

    private void OnEnable()
    {
        NeedFirstName = serializedObject.FindProperty("NeedFirstName");
        NeedLastName = serializedObject.FindProperty("NeedLastName");

        startRaces = serializedObject.FindProperty("startRaces");
        //   RaceList = serializedObject.FindProperty("assingRace.Options");

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

        reward = serializedObject.FindProperty("reward");
        rewardExp = serializedObject.FindProperty("reward.expReward");
        rewardGold = serializedObject.FindProperty("reward.goldReward");
        rewardRng = serializedObject.FindProperty("reward.rng");
        drops = serializedObject.FindProperty("reward.drops");
    }

    public override void OnInspectorGUI()
    {
        //GUILayout.Label("test");
        EnemyPrefab myTarget = (EnemyPrefab)target;
        nameFold = EditorGUILayout.Foldout(nameFold, "Name", true, EditorStyles.foldout);
        if (nameFold)
        {
            GUILayout.BeginVertical("Box");
            UgreEditorTools.TwoBoldLabels("First name", "Last name");
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
            UgreEditorTools.TwoBoldLabels("Race", "Amount");
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
            UgreEditorTools.TwoBoldLabels("Assing str", "Assing charm");
            TwoIntSliders(assingStr, assingCharm);
            UgreEditorTools.TwoBoldLabels("Assing end", "Assing dex");
            TwoIntSliders(assingEnd, assingDex);
            UgreEditorTools.TwoBoldLabels("Assing int", "Stats rng factor");
            IntAndFloatSlider(assingInt, statRngFactor);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        bodyStats = EditorGUILayout.Foldout(bodyStats, "Stats", true, EditorStyles.foldout);
        if (bodyStats)
        {
            EditorGUILayout.BeginVertical("Box");
            serializedObject.Update();
            UgreEditorTools.TwoBoldLabels("Height", "Height rng");
            IntAndFloatSlider(assingHeight, heightRng);
            UgreEditorTools.TwoBoldLabels("Fat", "Fat rng");
            IntAndFloatSlider(assingFat, fatRng);
            UgreEditorTools.TwoBoldLabels("Muscle", "Muscle rng");
            IntAndFloatSlider(assingMuscle, muscleRng);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        rewardFold = EditorGUILayout.Foldout(rewardFold, "Rewards", true, EditorStyles.foldout);
        if (rewardFold)
        {
            EditorGUILayout.BeginVertical("Box");
            UgreEditorTools.TwoBoldLabels("Exp reward", "Gold reward");
            serializedObject.Update();
            EditorGUILayout.BeginHorizontal();
            rewardExp.intValue = EditorGUILayout.IntField(rewardExp.intValue);
            rewardGold.intValue = EditorGUILayout.IntField(rewardGold.intValue);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.LabelField("Rng", EditorStyles.boldLabel);
            rewardRng.floatValue = EditorGUILayout.Slider(rewardRng.floatValue, 0, 1f);
            if (GUILayout.Button("Add drop"))
            {
                drops.arraySize++;
            }
            for (int i = 0; i < drops.arraySize; i++)
            {
                var drop = drops.GetArrayElementAtIndex(i);
                EditorGUILayout.PropertyField(drop, new GUIContent("Drop " + i), true);
            }
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.PropertyField(reward);

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

    private void TwoIntSliders(SerializedProperty one, SerializedProperty two)
    {
        EditorGUILayout.BeginHorizontal();
        one.intValue = EditorGUILayout.IntSlider(one.intValue, 0, 99);
        two.intValue = EditorGUILayout.IntSlider(two.intValue, 0, 99);
        EditorGUILayout.EndHorizontal();
    }

    private void TwoIntSliders(SerializedProperty one, SerializedProperty two, int maxVal)
    {
        EditorGUILayout.BeginHorizontal();
        one.intValue = EditorGUILayout.IntSlider(one.intValue, 0, maxVal);
        two.intValue = EditorGUILayout.IntSlider(two.intValue, 0, maxVal);
        EditorGUILayout.EndHorizontal();
    }

    private void TwoIntSliders(SerializedProperty one, SerializedProperty two, int minVal, int maxVal)
    {
        EditorGUILayout.BeginHorizontal();
        one.intValue = EditorGUILayout.IntSlider(one.intValue, minVal, maxVal);
        two.intValue = EditorGUILayout.IntSlider(two.intValue, minVal, maxVal);
        EditorGUILayout.EndHorizontal();
    }
}

public static class UgreEditorTools
{
    public static void TwoBoldLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(secondLabel, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
    }
}