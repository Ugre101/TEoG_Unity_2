using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Items))]
public class ItemsEditor : Editor
{
    private Items items;
    private SerializedProperty list;
    private Rect dropArea;
    public override void OnInspectorGUI()
    {
        list = serializedObject.FindProperty("items");
        items = (Items)target;
        dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUILayout.Space(5);
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
                    foreach(Object dragged in DragAndDrop.objectReferences)
                    {
                        if (dragged is Item)
                        {
                            items.Add((Item)dragged);
                        }
                    }
                }
                break;
        }
    }
}
