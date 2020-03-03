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

    public override void OnInspectorGUI()
    {
        // base.OnInspectorGUI();
        serializedObject.Update();
        UgreEditorTools.TextAreaWithBoldLabel(perkInfo, "Perk info");
        UgreEditorTools.TextAreaWithBoldLabel(perkEffects, "Perk effects");
        UgreEditorTools.TwoBoldLabels("Max level", "Perk cost");
        UgreEditorTools.TwoIntSliders(maxLevel, perkCost);
        NeedStat = EditorGUILayout.Toggle("Have stat req", NeedStat);
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
                var ele = neededCharStats.GetArrayElementAtIndex(i);
                var amount = ele.FindPropertyRelative("amount");
                var stat = ele.FindPropertyRelative("stat");
                EditorGUILayout.IntField("Amount", amount.intValue);
                EditorGUILayout.PropertyField(stat);
                if (GUILayout.Button("Remove"))
                {
                    neededCharStats.DeleteArrayElementAtIndex(i);
                }
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndVertical();
        }

        serializedObject.ApplyModifiedProperties();
    }
}