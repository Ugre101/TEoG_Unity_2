using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EnemyPrefab))]
public class EnemyPrefabProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
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
}