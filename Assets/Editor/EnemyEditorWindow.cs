using UnityEditor;

public class EnemyEditorWindow : EditorWindow
{
    private static EnemyHolder holder;

    public static void Open(EnemyHolder charHolder)
    {
        BasicCharEditorWindow window = GetWindow<BasicCharEditorWindow>("Enemy prefab");
        holder = charHolder;
    }

    private void OnGUI()
    {
        Editor objectEditor = Editor.CreateEditor(holder);

        objectEditor.DrawDefaultInspector();
    }
}