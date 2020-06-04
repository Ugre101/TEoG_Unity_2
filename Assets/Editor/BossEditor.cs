using UnityEditor;

public static class BossEditorBools
{
    public static bool BossStuffFoldout { get; set; } = false;
}

[CustomEditor(typeof(Boss))]
public class BossEditor : EnemyPrefabEditor
{
    private bool BossStuff { get => BossEditorBools.BossStuffFoldout; set => BossEditorBools.BossStuffFoldout = value; }
    private SerializedProperty hasPreBattleDialog, hasPostBattleDialog, hasCustomScene;

    private void OnEnable()
    {
        GetSerializedObjectsForEnemyPrefab();
        hasPreBattleDialog = serializedObject.FindProperty("hasPreBattleDialog");
        hasPostBattleDialog = serializedObject.FindProperty("hasPostBattleDialog");
        hasCustomScene = serializedObject.FindProperty("hasCustomScene");
    }

    public override void OnInspectorGUI()
    {
        BossStuff = EditorGUILayout.Foldout(BossStuff, "Boss stuff", true, EditorStyles.foldout);
        if (BossStuff)
        {
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.BeginHorizontal();
            serializedObject.UpdateIfRequiredOrScript();
            EditorGUILayout.PropertyField(hasPreBattleDialog);
            EditorGUILayout.PropertyField(hasPostBattleDialog);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.PropertyField(hasCustomScene);
            serializedObject.ApplyModifiedProperties();
            EditorGUILayout.EndVertical();
        }
        base.OnInspectorGUI();
    }
}