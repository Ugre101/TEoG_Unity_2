using System.IO;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ItemHolder))]
public class ItemHolderEditor : Editor
{
    private ItemHolder items;
    private SerializedProperty list;
    private Rect dropArea;

    private void OnEnable()
    {
        items = (ItemHolder)target;
        list = serializedObject.FindProperty("items");
        FindItems();
    }

    public override void OnInspectorGUI()
    {
        dropArea = GUILayoutUtility.GetRect(0.0f, 70.0f, GUILayout.ExpandWidth(true));
        serializedObject.Update();
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
        }
        UgreEditorTools.DropAreaGUI(dropArea, "Drop new item", HandleItem);
        serializedObject.ApplyModifiedProperties();
        // base.OnInspectorGUI();
    }

    private void HandleItem(Object obj)
    {
        if (obj is Item item)
        {
            items.Add(item);
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
                    EditorUtility.SetDirty(items); // I hope this makes added items stick trough builds
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }
}