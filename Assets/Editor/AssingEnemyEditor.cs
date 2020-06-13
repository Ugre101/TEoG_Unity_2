using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AssingEnemy))]
public class AssingEnemyEditor : Editor
{
    #region FoldsShortcut

    private bool NameFold { get => EnemyPrefabEditorFoldouts.NameFold; set => EnemyPrefabEditorFoldouts.NameFold = value; }
    private bool RaceFold { get => EnemyPrefabEditorFoldouts.RaceFold; set => EnemyPrefabEditorFoldouts.RaceFold = value; }
    private bool GenderFold { get => EnemyPrefabEditorFoldouts.GenderFold; set => EnemyPrefabEditorFoldouts.GenderFold = value; }
    private bool StatsFold { get => EnemyPrefabEditorFoldouts.StatsFold; set => EnemyPrefabEditorFoldouts.StatsFold = value; }
    private bool BodyFold { get => EnemyPrefabEditorFoldouts.BodyFold; set => EnemyPrefabEditorFoldouts.BodyFold = value; }
    private bool RewardFold { get => EnemyPrefabEditorFoldouts.RewardFold; set => EnemyPrefabEditorFoldouts.RewardFold = value; }
    private bool IsQuestFold { get => EnemyPrefabEditorFoldouts.IsQuestFold; set => EnemyPrefabEditorFoldouts.IsQuestFold = value; }
    private bool showStandard { get => EnemyPrefabEditorFoldouts.ShowStandardEditor; set => EnemyPrefabEditorFoldouts.ShowStandardEditor = value; }

    #endregion FoldsShortcut

    #region SerializedProperties

    private SerializedProperty firstName, lastName, NeedFirstName, NeedLastName;
    private SerializedProperty startRaces;
    private SerializedProperty genderAmount, genderLockBool, genderLocked, genderTypeBool, genderType;
    private SerializedProperty assingStr, assingCharm, assingEnd, assingDex, assingInt, assingWillpower, statRngFactor;
    private SerializedProperty assingHeight, heightRng, assingFat, fatRng, assingMuscle, muscleRng;
    private SerializedProperty rewardExp, rewardGold, rewardRng, drops;
    private SerializedProperty isQuestBool, isQuestEnum;

    #endregion SerializedProperties

    private int TotalStats => assingStr.intValue + assingCharm.intValue + assingEnd.intValue + assingDex.intValue + assingInt.intValue + assingWillpower.intValue;

    protected void SetBaseLevel(int setVal)
    {
        assingStr.intValue = setVal + 6;
        assingCharm.intValue = setVal + 6;
        assingEnd.intValue = setVal + 6;
        assingDex.intValue = setVal + 6;
        assingInt.intValue = setVal + 6;
        assingWillpower.intValue = setVal + 6;
        genderAmount.floatValue = (setVal + setVal) * 100;
        rewardExp.intValue = setVal * 15;
        rewardGold.intValue = setVal * 10;
    }

    protected string StatEndResult(int stat, float rng)
    {
        int min = Mathf.FloorToInt(stat * (1f - rng));
        int max = Mathf.FloorToInt(stat * (1f + rng));
        return $"{min} - {max}";
    }

    private void OnEnable()
    {
        GetSerializedObjectsForEnemyPrefab();
    }

    protected void GetSerializedObjectsForEnemyPrefab()
    {
        firstName = serializedObject.FindProperty("firstName");
        lastName = serializedObject.FindProperty("lastName");
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
        AssingEnemy myTarget = (AssingEnemy)target;
        EditorGUILayout.LabelField("Set base stat for level", EditorStyles.boldLabel);
        serializedObject.Update();
        EditorGUILayout.BeginHorizontal();
        for (int i = 1; i < 14; i++)
        {
            if (GUILayout.Button((i + (i - 1)).ToString()))
            {
                SetBaseLevel(i);
            }
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        for (int i = 14; i < 27; i++)
        {
            if (GUILayout.Button((i + (i - 1)).ToString()))
            {
                SetBaseLevel(i + 6);
            }
        }
        EditorGUILayout.EndHorizontal();
        serializedObject.ApplyModifiedProperties();
        NameFold = EditorGUILayout.Foldout(NameFold, "Name", true, EditorStyles.foldout);
        if (NameFold)
        {
            GUILayout.BeginVertical("Box");
            UgreEditorTools.TwoBoldLabels("First name", "Last name");
            GUILayout.BeginHorizontal();
            //   Identity identity = myTarget.BasicChar.Identity;
            firstName.stringValue = EditorGUILayout.TextArea(firstName.stringValue);
            lastName.stringValue = EditorGUILayout.TextArea(lastName.stringValue);
            GUILayout.EndHorizontal();
            GUILayout.BeginVertical("Box");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Give random first name");
            serializedObject.Update();
            NeedFirstName.boolValue = EditorGUILayout.Toggle(NeedFirstName.boolValue);
            GUILayout.Label("Give random last name");
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
            EditorGUILayout.LabelField("Total stat points: " + TotalStats.ToString());
            int level = 1 + Mathf.CeilToInt((TotalStats - 42) / 3);
            EditorGUILayout.LabelField("Level: " + level);
            EditorGUILayout.EndHorizontal();
            serializedObject.Update();
            UgreEditorTools.TwoBoldLabels("Assing str", "Assing charm");
            UgreEditorTools.TwoIntSliders(assingStr, assingCharm);
            UgreEditorTools.TwoBoldLabels("Assing end", "Assing dex");
            UgreEditorTools.TwoIntSliders(assingEnd, assingDex);
            UgreEditorTools.TwoBoldLabels("Assing int", "Assing Willpower");
            UgreEditorTools.TwoIntSliders(assingInt, assingWillpower);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stat rng: ", EditorStyles.boldLabel);
            statRngFactor.floatValue = EditorGUILayout.Slider(statRngFactor.floatValue, 0, 0.99f);
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Stats end result", EditorStyles.boldLabel);
            EditorGUILayout.EndHorizontal();
            UgreEditorTools.TwoBoldLabels("Str: " + StatEndResult(assingStr.intValue, statRngFactor.floatValue), "Charm: " + StatEndResult(assingCharm.intValue, statRngFactor.floatValue));
            UgreEditorTools.TwoBoldLabels("End: " + StatEndResult(assingEnd.intValue, statRngFactor.floatValue), "Dex: " + StatEndResult(assingDex.intValue, statRngFactor.floatValue));
            UgreEditorTools.TwoBoldLabels("Int: " + StatEndResult(assingInt.intValue, statRngFactor.floatValue), "Will: " + StatEndResult(assingWillpower.intValue, statRngFactor.floatValue));
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndVertical();
        }
        BodyFold = EditorGUILayout.Foldout(BodyFold, "Body stats", true, EditorStyles.foldout);
        if (BodyFold)
        {
            EditorGUILayout.BeginVertical("Box");
            serializedObject.Update();
            UgreEditorTools.TwoBoldLabels("Height", "Height rng");
            UgreEditorTools.IntAndFloatSlider(assingHeight, heightRng);
            UgreEditorTools.TwoBoldLabels("Fat", "Fat rng");
            UgreEditorTools.IntAndFloatSlider(assingFat, fatRng);
            UgreEditorTools.TwoBoldLabels("Muscle", "Muscle rng");
            UgreEditorTools.IntAndFloatSlider(assingMuscle, muscleRng);
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
        showStandard = EditorGUILayout.Foldout(showStandard, "Show standard editor view", true, EditorStyles.foldout);
        if (showStandard)
        {
            GUILayout.Space(20);
            base.OnInspectorGUI();
        }
    }
}