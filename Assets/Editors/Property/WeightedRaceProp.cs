using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
[CustomPropertyDrawer(typeof(WeightedRace))]
public class WeightedRaceProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      //  EditorGUI.PropertyField(position, property, label, true);
        EditorGUI.BeginProperty(position, label, property);
       
       // property.DeleteCommand();
        var raceRect = new Rect(position.x, position.y, 80, position.height);
        var sliderRect = new Rect(raceRect.xMax, position.y,position.width - (raceRect.width + 20), position.height);
        // var weightRect = new Rect(sliderRect.xMax, position.y, 50, position.height);
        var btnRect = new Rect(sliderRect.xMax, position.y, 20, position.height);
        var weight = property.FindPropertyRelative("Weight");
     
        EditorGUI.PropertyField(raceRect, property.FindPropertyRelative("Race"), GUIContent.none);
        // EditorGUI.PropertyField(sliderRect, property.FindPropertyRelative("Weight"), GUIContent.none);
        weight.intValue = EditorGUI.IntSlider(sliderRect, weight.intValue, 1, 10);
        if (GUI.Button(btnRect,"X"))
        {
            property.DeleteCommand();
        }
      //  weight.intValue = EditorGUI.IntField(weightRect,weight.intValue);
        EditorGUI.EndProperty();    
        
        //base.OnGUI(position, property, label);
    }
  /*  public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        var raceField = new PropertyField(property.FindPropertyRelative("Race"));
        container.Add(raceField);
        return container;
    }*/
}
