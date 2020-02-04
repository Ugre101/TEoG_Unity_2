using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemDrop))]
public class ItemDropProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var itemRect = new Rect(position.x, position.y, position.width / 3, position.height);
        var dropLabel = new Rect(itemRect.xMax, position.y, position.width / 3, position.height);
        var dropChanceRect = new Rect(dropLabel.xMax, position.y, position.width / 4, position.height);
        var btnRect = new Rect(dropChanceRect.xMax, position.y, 20, position.height);
        EditorGUI.PropertyField(itemRect, property.FindPropertyRelative("item"), GUIContent.none);
        EditorGUI.LabelField(dropLabel, "Drop chance: ");
        EditorGUI.PropertyField(dropChanceRect, property.FindPropertyRelative("dropChance"), GUIContent.none);
        if (GUI.Button(btnRect, "X"))
        {
            property.DeleteCommand();
        }

        EditorGUI.EndProperty();
    }
}