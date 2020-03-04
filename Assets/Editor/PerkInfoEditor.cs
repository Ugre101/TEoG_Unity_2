using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PerkInfo))]
public class PerkInfoEditor : Editor
{
    private SerializedProperty perkInfo, perkEffects, maxLevel, perkCost, needCharStat, neededCharStats, needOtherPerks, neededPerks;

    #region Assing SerProps

    private void OnEnable()
    {
        perkInfo = serializedObject.FindProperty("perkInfo");
        perkEffects = serializedObject.FindProperty("perkEffects");
        maxLevel = serializedObject.FindProperty("maxLevel");
        perkCost = serializedObject.FindProperty("perkCost");
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
        // base.OnInspectorGUI();
        serializedObject.Update();
        UgreEditorTools.TextAreaWithBoldLabel(perkInfo, "Perk info");
        UgreEditorTools.TextAreaWithBoldLabel(perkEffects, "Perk effects");
        UgreEditorTools.TwoBoldLabels("Max level", "Perk cost");
        UgreEditorTools.TwoIntSliders(maxLevel, perkCost);
        NeedStat = EditorGUILayout.Toggle("Have stat requirements", NeedStat);
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
        NeedPerk = EditorGUILayout.Toggle("Have perk requirements", NeedPerk);
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
                EditorGUILayout.PropertyField(ele, GUIContent.none);
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