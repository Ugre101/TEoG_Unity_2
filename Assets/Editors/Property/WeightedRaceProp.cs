using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(WeightedRace))]
public class WeightedRaceProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);
        var raceRect = new Rect(position.x, position.y, 80, position.height);
        var sliderRect = new Rect(raceRect.xMax, position.y, position.width - (raceRect.width + 20), position.height);
        var btnRect = new Rect(sliderRect.xMax, position.y, 20, position.height);
        var weight = property.FindPropertyRelative("Weight");

        EditorGUI.PropertyField(raceRect, property.FindPropertyRelative("Race"), GUIContent.none);
        weight.intValue = EditorGUI.IntSlider(sliderRect, weight.intValue, 1, 10);
        if (GUI.Button(btnRect, "X"))
        {
            property.DeleteCommand();
        }
        EditorGUI.EndProperty();
    }
}