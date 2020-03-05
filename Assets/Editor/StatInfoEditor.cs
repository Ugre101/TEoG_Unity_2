using UnityEditor;

[CustomEditor(typeof(StatInfo))]
public class StatInfoEditor : BaseInfoEditor
{
    #region Assing SerProps

    private SerializedProperty stat;

    private void OnEnable()
    {
        stat = serializedObject.FindProperty("stat");
        BaseEnable();
    }

    #endregion Assing SerProps

    public override void OnInspectorGUI()
    {
        BaseDraw();
        serializedObject.Update();
        EditorGUILayout.PropertyField(stat);
        serializedObject.ApplyModifiedProperties();
    }
}