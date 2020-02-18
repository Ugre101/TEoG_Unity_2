using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(CharSprite))]
public class CharSpriteProp : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        UgreEditorTools.ProperyWithDeleteBtn(position, label, property);
    }
}