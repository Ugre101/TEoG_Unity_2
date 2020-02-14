using UnityEditor;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    public Item item;

    public override void OnInspectorGUI()
    {
        item = (Item)target;
        EditorGUILayout.LabelField("Summary");
        EditorGUILayout.BeginVertical("Box");
        UgreEditorTools.TwoLabels("Title: " + item.Title, "Id: " + item.ItemId);
        UgreEditorTools.TwoLabels("Type: " + item.Type, "Use: " + item.UseName);
        EditorGUILayout.LabelField(item.Desc, EditorStyles.textArea);
        EditorGUILayout.EndVertical();
        base.OnInspectorGUI();
    }
}