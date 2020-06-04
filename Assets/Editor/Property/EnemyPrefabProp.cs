using UnityEditor;
using UnityEngine;

namespace EditorStuff
{
    [CustomPropertyDrawer(typeof(EnemyHolder))]
    public class EnemyPrefabProp : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UgreEditorTools.ProperyWithDeleteBtn(position, label, property);
        }
    }

    [CustomPropertyDrawer(typeof(Boss))]
    public class BossProp : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            UgreEditorTools.ProperyWithDeleteBtn(position, label, property);
        }
    }
}