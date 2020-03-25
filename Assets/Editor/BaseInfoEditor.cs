using UnityEditor;

public abstract class BaseInfoEditor : Editor
{
    protected SerializedProperty icon, perkTitle, perkInfo, perkEffects, maxLevel, perkCost;

    protected void BaseEnable()
    {
        icon = serializedObject.FindProperty("icon");
        perkTitle = serializedObject.FindProperty("perkTitle");
        perkInfo = serializedObject.FindProperty("perkInfo");
        perkEffects = serializedObject.FindProperty("perkEffects");
        maxLevel = serializedObject.FindProperty("maxLevel");
        perkCost = serializedObject.FindProperty("perkCost");
    }

    protected void BaseDraw()
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script: ", MonoScript.FromScriptableObject((BaseInfo)target), typeof(BaseInfo), false);
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.BeginVertical();
        serializedObject.Update();
        EditorGUILayout.PropertyField(icon);
        UgreEditorTools.TextAreaWithBoldLabel(perkTitle, "Perk title");
        UgreEditorTools.TextAreaWithBoldLabel(perkInfo, "Perk info");
        UgreEditorTools.TextAreaWithBoldLabel(perkEffects, "Perk effects");
        UgreEditorTools.TwoBoldLabels("Max level", "Perk cost");
        UgreEditorTools.TwoIntSliders(maxLevel, perkCost);
        serializedObject.ApplyModifiedProperties();
        EditorGUILayout.EndVertical();
    }
}