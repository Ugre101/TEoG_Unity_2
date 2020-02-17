using UnityEditor;
using UnityEngine;

// Static container so foldouts doesn't close dureing refresh
public static class EnemyPrefabEditorFoldouts
{
    public static bool NameFold { get; set; } = false;
    public static bool RaceFold { get; set; } = false;
    public static bool GenderFold { get; set; } = false;
    public static bool StatsFold { get; set; } = false;
    public static bool BodyFold { get; set; } = false;
    public static bool RewardFold { get; set; } = false;
    public static bool IsQuestFold { get; set; } = false;
}

[CustomEditor(typeof(EnemyPrefab))]
public class EnemyPrefabEditor : BasicCharEditor
{
    private bool NameFold { get => EnemyPrefabEditorFoldouts.NameFold; set => EnemyPrefabEditorFoldouts.NameFold = value; }
    private bool RaceFold { get => EnemyPrefabEditorFoldouts.RaceFold; set => EnemyPrefabEditorFoldouts.RaceFold = value; }
    private bool GenderFold { get => EnemyPrefabEditorFoldouts.GenderFold; set => EnemyPrefabEditorFoldouts.GenderFold = value; }
    private bool StatsFold { get => EnemyPrefabEditorFoldouts.StatsFold; set => EnemyPrefabEditorFoldouts.StatsFold = value; }
    private bool BodyFold { get => EnemyPrefabEditorFoldouts.BodyFold; set => EnemyPrefabEditorFoldouts.BodyFold = value; }
    private bool RewardFold { get => EnemyPrefabEditorFoldouts.RewardFold; set => EnemyPrefabEditorFoldouts.RewardFold = value; }
    private bool IsQuestFold { get => EnemyPrefabEditorFoldouts.IsQuestFold; set => EnemyPrefabEditorFoldouts.IsQuestFold = value; }
    private SerializedProperty NeedFirstName, NeedLastName;
    private SerializedProperty startRaces;
    private SerializedProperty genderAmount, genderLockBool, genderLocked, genderTypeBool, genderType;
    private SerializedProperty assingStr, assingCharm, assingEnd, assingDex, assingInt, assingWillpower, statRngFactor;
    private SerializedProperty assingHeight, heightRng, assingFat, fatRng, assingMuscle, muscleRng;
    private SerializedProperty rewardExp, rewardGold, rewardRng, drops;
    private SerializedProperty isQuestBool, isQuestEnum;
    private int TotalStats => assingStr.intValue + assingCharm.intValue + assingEnd.intValue + assingDex.intValue + assingInt.intValue + assingWillpower.intValue;

    private void OnEnable()
    {
        NeedFirstName = serializedObject.FindProperty("NeedFirstName");
        NeedLastName = serializedObject.FindProperty("NeedLastName");

        startRaces = serializedObject.FindProperty("startRaces");
        //   RaceList = serializedObject.FindProperty("assingRace.Options");
        genderAmount = serializedObject.FindProperty("startGender.amount");
        genderLockBool = serializedObject.FindProperty("startGender.genderLock");
        genderLocked = serializedObject.FindProperty("startGender.lockedGender");
        genderTypeBool = serializedObject.FindProperty("startGender.favoured");
        genderType = serializedObject.FindProperty("startGender.favouredGenderType");
        assingStr = serializedObject.FindProperty("assingStr");
        assingCharm = serializedObject.FindProperty("assingCharm");
        assingEnd = serializedObject.FindProperty("assingEnd");
        assingDex = serializedObject.FindProperty("assingDex");
        assingInt = serializedObject.FindProperty("assingInt");
        assingWillpower = serializedObject.FindProperty("assingWill");
        statRngFactor = serializedObject.FindProperty("statRngFactor");

        assingHeight = serializedObject.FindProperty("assingHeight");
        heightRng = serializedObject.FindProperty("heightRng");
        assingFat = serializedObject.FindProperty("assingFat");
        fatRng = serializedObject.FindProperty("fatRng");
        assingMuscle = serializedObject.FindProperty("assingMuscle");
        muscleRng = serializedObject.FindProperty("muscleRng");

        rewardExp = serializedObject.FindProperty("reward.expReward");
        rewardGold = serializedObject.FindProperty("reward.goldReward");
        rewardRng = serializedObject.FindProperty("reward.rng");
        drops = serializedObject.FindProperty("reward.drops");

        isQuestBool = serializedObject.FindProperty("isQuest.isQuest");
        isQuestEnum = serializedObject.FindProperty("isQuest.quest");
    }

    public override void OnInspectorGUI()
    {
        //GUILayout.Label("test");
        EnemyPrefab myTarget = (EnemyPrefab)target;
        NameFold = EditorGUILayout.Foldout(NameFold, "Name", true, EditorStyles.foldout);
        if (NameFold)
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
        RaceFold = EditorGUILayout.Foldout(RaceFold, "Assing race", true, EditorStyles.foldout);
        if (RaceFold)
        {
            GUILayout.BeginVertical("Box");
            serializedObject.Update();
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
        GenderFold = EditorGUILayout.Foldout(GenderFold, "Gender", true, EditorStyles.foldout);
        if (GenderFold)
        {
            GUILayout.BeginVertical("Box");
            serializedObject.Update();
            GUILayout.BeginVertical();
            genderAmount.floatValue = EditorGUILayout.FloatField("Essence amount", genderAmount.floatValue);
            GUILayout.EndVertical();
            GUILayout.BeginHorizontal();
            genderLockBool.boolValue = EditorGUILayout.Toggle("Gender lock", genderLockBool.boolValue);
            if (genderLockBool.boolValue)
            {
                genderTypeBool.boolValue = false;
            }
            genderTypeBool.boolValue = EditorGUILayout.Toggle("Favourd gender type", genderTypeBool.boolValue);
            if (genderTypeBool.boolValue)
            {
                genderLockBool.boolValue = false;
            }
            GUILayout.EndHorizontal();
            if (genderLockBool.boolValue)
            {
                GUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(genderLocked);
                GUILayout.EndVertical();
            }
            if (genderTypeBool.boolValue)
            {
                GUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(genderType);
                GUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
            GUILayout.EndVertical();
        }
        StatsFold = EditorGUILayout.Foldout(StatsFold, "Stats", true, EditorStyles.foldout);
        if (StatsFold)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Total stat points: ");
            EditorGUILayout.IntField(TotalStats);
            EditorGUILayout.LabelField("Level: " + (TotalStats - 22) / 4);
            EditorGUILayout.EndHorizontal();
            serializedObject.Update();
            UgreEditorTools.TwoBoldLabels("Assing str", "Assing charm");
            TwoIntSliders(assingStr, assingCharm);
            UgreEditorTools.TwoBoldLabels("Assing end", "Assing dex");
            TwoIntSliders(assingEnd, assingDex);
            UgreEditorTools.TwoBoldLabels("Assing int", "Assing Willpower");
            TwoIntSliders(assingInt, assingWillpower);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stat rng: ", EditorStyles.boldLabel);
            statRngFactor.floatValue = EditorGUILayout.Slider(statRngFactor.floatValue, 0, 0.99f);
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        BodyFold = EditorGUILayout.Foldout(BodyFold, "Body stats", true, EditorStyles.foldout);
        if (BodyFold)
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
        RewardFold = EditorGUILayout.Foldout(RewardFold, "Rewards", true, EditorStyles.foldout);
        if (RewardFold)
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
        IsQuestFold = EditorGUILayout.Foldout(IsQuestFold, "Is Quest", true, EditorStyles.foldout);
        if (IsQuestFold)
        {
            EditorGUILayout.BeginVertical("Box");
            serializedObject.Update();
            isQuestBool.boolValue = EditorGUILayout.Toggle("Is quest", isQuestBool.boolValue);
            if (isQuestBool.boolValue)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.PropertyField(isQuestEnum);
                EditorGUILayout.EndVertical();
            }
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
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

    private void TwoIntSliders(SerializedProperty one, SerializedProperty two) => TwoIntSliders(one, two, 0, 99);

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
    public static void TwoLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel);
        EditorGUILayout.LabelField(secondLabel);
        EditorGUILayout.EndHorizontal();
    }

    public static void TwoBoldLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(secondLabel, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
    }

    public static void IntAndFloatSlider(SerializedProperty intSlider, SerializedProperty floatSlider)
    {
        EditorGUILayout.BeginHorizontal();
        intSlider.intValue = EditorGUILayout.IntSlider(intSlider.intValue, 0, 500);
        floatSlider.floatValue = EditorGUILayout.Slider(floatSlider.floatValue, 0f, 1f);
        EditorGUILayout.EndHorizontal();
    }

    public static void TwoIntSliders(SerializedProperty one, SerializedProperty two) => TwoIntSliders(one, two, 0, 99);

    public static void TwoIntSliders(SerializedProperty one, SerializedProperty two, int maxVal) => TwoIntSliders(one, two, 0, maxVal);

    public static void TwoIntSliders(SerializedProperty one, SerializedProperty two, int minVal, int maxVal)
    {
        EditorGUILayout.BeginHorizontal();
        one.intValue = EditorGUILayout.IntSlider(one.intValue, minVal, maxVal);
        two.intValue = EditorGUILayout.IntSlider(two.intValue, minVal, maxVal);
        EditorGUILayout.EndHorizontal();
    }
}