using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Items))]
public class ItemsEditor : Editor
{
    private Items items;
    private SerializedProperty list;
    private Rect dropArea;

    private void OnEnable()
    {
        items = (Items)target;
        list = serializedObject.FindProperty("items");
        FindItems();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(5);
        dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        serializedObject.Update();
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
        }
        serializedObject.ApplyModifiedProperties();
        DropAreaGUI();
        // base.OnInspectorGUI();
    }

    public void DropAreaGUI()
    {
        Event evt = Event.current;
        GUI.Box(dropArea, "Drop new item");
        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                {
                    return;
                }
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (Object dragged in DragAndDrop.objectReferences)
                    {
                        if (dragged is Item item)
                        {
                            items.Add(item);
                        }
                    }
                }
                break;
        }
    }

    private void FindItems()
    {
        string assetPath = AssetDatabase.GetAssetPath(items);
        string fileName = Path.GetFileName(assetPath);
        string dictName = assetPath.Replace(fileName, "");
        string folderName = dictName + "toInclude";
        DirectoryInfo toInclude = Directory.Exists(folderName) ? new DirectoryInfo(folderName) : Directory.CreateDirectory(folderName);
        foreach (FileInfo fileInfo in toInclude.GetFiles())
        {
            var temp = AssetDatabase.LoadAssetAtPath(folderName + "/" + fileInfo.Name, typeof(Item));
            if (temp is Item item)
            {
                if (!items.HasItem(item.ItemId))
                {
                    items.Add(item);
                }
            }
        }
    }
}