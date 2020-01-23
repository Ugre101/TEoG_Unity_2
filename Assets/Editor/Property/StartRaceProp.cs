using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(StartRace))]
public class StartRaceProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var raceRect = new Rect(position.x, position.y, position.width / 2, position.height);
        var amountRect = new Rect(raceRect.xMax, position.y, position.width / 2 - 25, position.height);
        var btnRect = new Rect(amountRect.xMax, position.y, 25, position.height);
        EditorGUI.PropertyField(raceRect, property.FindPropertyRelative("races"), GUIContent.none);
        EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
        if (GUI.Button(btnRect, "X"))
        {
            property.DeleteCommand();
        }
        EditorGUI.EndProperty();
    }
}