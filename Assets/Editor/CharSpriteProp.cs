using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CharSprite))]
public class CharSpriteProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        Rect propRect = new Rect(position.x, position.y, position.width - 20, position.height);
        EditorGUI.PropertyField(propRect, property, GUIContent.none);
        Rect btnRect = new Rect(propRect.xMax, position.y, 20, position.height);
        if (GUI.Button(btnRect, "X"))
        {
            property.DeleteCommand();
            property.DeleteCommand();
        }
        EditorGUI.EndProperty();
    }
}