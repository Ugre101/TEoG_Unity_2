using UnityEditor;

public static class BossEditorBools
{
    public static bool BossStuffFoldout { get; set; } = false;
}

[CustomEditor(typeof(Boss))]
public class BossEditor : EnemyPrefabEditor
{
    private bool BossStuff { get => BossEditorBools.BossStuffFoldout; set => BossEditorBools.BossStuffFoldout = value; }

    public override void OnInspectorGUI()
    {
        BossStuff = EditorGUILayout.Foldout(BossStuff, "Boss stuff", true, EditorStyles.foldout);
        if (BossStuff)
        {
        }
        base.OnInspectorGUI();
    }
}