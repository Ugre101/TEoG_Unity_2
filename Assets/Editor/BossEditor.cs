using UnityEditor;

public static class BossEditorBools
{
    public static bool BossStuffFoldout { get; set; } = false;
}

[CustomEditor(typeof(AssingBoss))]
public class AssingBossEditor : AssingEnemyEditor
{

    private AssingBoss boss;
    private bool BossStuff { get => BossEditorBools.BossStuffFoldout; set => BossEditorBools.BossStuffFoldout = value; }
    private SerializedProperty hasPreBattleDialog, hasPostBattleDialog, hasCustomScene;

    private void OnEnable() => Init();

    private void Init()
    {
        GetSerializedObjectsForEnemyPrefab();
        hasCustomScene = serializedObject.FindProperty("hasCustomScene");
        hasPreBattleDialog = serializedObject.FindProperty("hasPreBattleDialog");
        hasPostBattleDialog = serializedObject.FindProperty("hasPostBattleDialog");
    }

    public override void OnInspectorGUI()
    {
        boss = (AssingBoss)target;
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