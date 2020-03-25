using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PerkInfo))]
public class PerkInfoEditor : BaseInfoEditor
{
    #region Assing SerProps

    private SerializedProperty perk, needCharStat, neededCharStats, needOtherPerks, neededPerks;

    private void OnEnable()
    {
        BaseEnable();
        perk = serializedObject.FindProperty("perk");
        needCharStat = serializedObject.FindProperty("needCharStat");
        neededCharStats = serializedObject.FindProperty("neededCharStats");
        needOtherPerks = serializedObject.FindProperty("needOtherPerks");
        neededPerks = serializedObject.FindProperty("neededPerks");
    }

    #endregion Assing SerProps

    private bool NeedStat { get => needCharStat.boolValue; set => needCharStat.boolValue = value; }
    private bool NeedPerk { get => needOtherPerks.boolValue; set => needOtherPerks.boolValue = value; }

    public override void OnInspectorGUI()
    {
        BaseDraw();
        serializedObject.Update();
        EditorGUILayout.PropertyField(perk);
        EditorGUILayout.BeginHorizontal();
        NeedStat = EditorGUILayout.Toggle("Have stat requirements", NeedStat);
        NeedPerk = EditorGUILayout.Toggle("Have perk requirements", NeedPerk);
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
        serializedObject.ApplyModifiedProperties();
    }
}