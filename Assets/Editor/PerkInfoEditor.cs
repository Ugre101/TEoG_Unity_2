using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PerkInfo))]
public class PerkInfoEditor : BaseInfoEditor
{
    #region Assing SerProps

    private SerializedProperty perk, needCharStat, neededCharStats, needOtherPerks, neededPerks, isExcluvsive, exclusiveWith;

    private void OnEnable()
    {
        BaseEnable();
        perk = serializedObject.FindProperty("perk");
        needCharStat = serializedObject.FindProperty("needCharStat");
        neededCharStats = serializedObject.FindProperty("neededCharStats");
        needOtherPerks = serializedObject.FindProperty("needOtherPerks");
        neededPerks = serializedObject.FindProperty("neededPerks");
        isExcluvsive = serializedObject.FindProperty("isExcluvsive");
        exclusiveWith = serializedObject.FindProperty("exclusiveWith");
    }

    #endregion Assing SerProps

    private bool NeedStat { get => needCharStat.boolValue; set => needCharStat.boolValue = value; }
    private bool NeedPerk { get => needOtherPerks.boolValue; set => needOtherPerks.boolValue = value; }
    private bool Exclusive { get => isExcluvsive.boolValue; set => isExcluvsive.boolValue = value; }

    public override void OnInspectorGUI()
    {
        BaseDraw();
        serializedObject.Update();
        EditorGUILayout.PropertyField(perk);
        EditorGUILayout.BeginHorizontal();
        NeedStat = EditorGUILayout.Toggle("Have stat requirements", NeedStat);
        NeedPerk = EditorGUILayout.Toggle("Have perk requirements", NeedPerk);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        Exclusive = EditorGUILayout.Toggle("Is exclusive perk", Exclusive);
        EditorGUILayout.EndHorizontal();
        if (NeedStat)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Stat requirements", EditorStyles.boldLabel);
            if (GUILayout.Button("Add"))
            {
                neededCharStats.arraySize++;
            }
            for (int i = 0; i < neededCharStats.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("box");
                SerializedProperty ele = neededCharStats.GetArrayElementAtIndex(i);
                SerializedProperty amount = ele.FindPropertyRelative("amount");
                SerializedProperty stat = ele.FindPropertyRelative("stat");
                amount.intValue = EditorGUILayout.IntField("Amount", amount.intValue);
                EditorGUILayout.PropertyField(stat);
                if (GUILayout.Button("Remove"))
                {
                    neededCharStats.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
        if (NeedPerk)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("Perk requirements", EditorStyles.boldLabel);
            if (GUILayout.Button("Add"))
            {
                neededPerks.arraySize++;
            }
            for (int i = 0; i < neededPerks.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("box");
                SerializedProperty ele = neededPerks.GetArrayElementAtIndex(i);
                SerializedProperty amount = ele.FindPropertyRelative("amount");
                SerializedProperty perksTypes = ele.FindPropertyRelative("perksTypes");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(perksTypes, GUIContent.none);
                amount.intValue = EditorGUILayout.IntField(amount.intValue);
                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Remove"))
                {
                    neededPerks.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
        if (Exclusive)
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField("If excluvsive with", EditorStyles.boldLabel);
            if (GUILayout.Button("Add"))
            {
                exclusiveWith.arraySize++;
            }
            for (int i = 0; i < exclusiveWith.arraySize; i++)
            {
                EditorGUILayout.BeginVertical("box");
                SerializedProperty ele = exclusiveWith.GetArrayElementAtIndex(i);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(ele,GUIContent.none);
                if (GUILayout.Button("Remove"))
                {
                    exclusiveWith.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }
        serializedObject.ApplyModifiedProperties();
    }
}

[CustomEditor(typeof(VorePerkInfo))]
public class VorePerkInfoEditor : PerkInfoEditor
{
    // Seem to be enough
}