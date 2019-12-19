using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Item), true)]
public class ItemEditor : Editor
{
    public Item item;

    public override void OnInspectorGUI()
    {
        item = (Item)target;
        EditorGUILayout.LabelField("Summary");
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Title: " + item.Title);
        EditorGUILayout.LabelField("Id: " + item.ItemId);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Type: " + item.Type);
        EditorGUILayout.LabelField("Use: " + item.UseName);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.TextArea(item.Desc, GUILayout.ExpandHeight(true));
        EditorGUILayout.EndVertical();
        base.OnInspectorGUI();
    }
}