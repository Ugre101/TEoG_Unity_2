using System;
using UnityEditor;
using UnityEngine;

public static class UgreEditorTools
{
    public static void TwoLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel);
        EditorGUILayout.LabelField(secondLabel);
        EditorGUILayout.EndHorizontal();
    }

    public static void TwoBoldLabels(string firstLabel, string secondLabel)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(firstLabel, EditorStyles.boldLabel);
        EditorGUILayout.LabelField(secondLabel, EditorStyles.boldLabel);
        EditorGUILayout.EndHorizontal();
    }

    public static void IntAndFloatSlider(SerializedProperty intSlider, SerializedProperty floatSlider)
    {
        EditorGUILayout.BeginHorizontal();
        intSlider.intValue = EditorGUILayout.IntSlider(intSlider.intValue, 0, 500);
        floatSlider.floatValue = EditorGUILayout.Slider(floatSlider.floatValue, 0f, 1f);
        EditorGUILayout.EndHorizontal();
    }

    public static void TwoIntSliders(SerializedProperty one, SerializedProperty two) => TwoIntSliders(one, two, 0, 99);

    public static void TwoIntSliders(SerializedProperty one, SerializedProperty two, int maxVal) => TwoIntSliders(one, two, 0, maxVal);

    public static void TwoIntSliders(SerializedProperty one, SerializedProperty two, int minVal, int maxVal)
    {
        EditorGUILayout.BeginHorizontal();
        one.intValue = EditorGUILayout.IntSlider(one.intValue, minVal, maxVal);
        two.intValue = EditorGUILayout.IntSlider(two.intValue, minVal, maxVal);
        EditorGUILayout.EndHorizontal();
    }

    public static void ProperyWithDeleteBtn(Rect position, GUIContent label, SerializedProperty property)
    {
        EditorGUI.BeginProperty(position, label, property);
        var itemRect = new Rect(position.x, position.y, position.width - 20, position.height);
        var btnRect = new Rect(itemRect.xMax, position.y, 20, position.height);
        EditorGUI.PropertyField(itemRect, property, GUIContent.none);
        if (GUI.Button(btnRect, "X"))
        {
            property.DeleteCommand();   // Delete propery itself
            property.DeleteCommand();   // Removes the now empty array index
        }
        EditorGUI.EndProperty();
    }

    public static void DropAreaGUI(Rect dropArea, string title, Action<UnityEngine.Object> dropped)
    {
        Event evt = Event.current;
        GUIStyle style = new GUIStyle(GUI.skin.box)
        {
            alignment = TextAnchor.MiddleCenter,
            fontStyle = FontStyle.Bold,
        };
        GUI.Box(dropArea, title, style);
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
                    foreach (UnityEngine.Object dragged in DragAndDrop.objectReferences)
                    {
                        dropped.Invoke(dragged);
                    }
                }
                break;
        }
    }
}