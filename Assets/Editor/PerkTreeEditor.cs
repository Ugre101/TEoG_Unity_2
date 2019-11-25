using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PerkTree))]
public class PerkTreeEditor : Editor
{
    private PerkTree perkTree;
    public override void OnInspectorGUI()
    {
        perkTree = (PerkTree)target;
        base.OnInspectorGUI();
        if (GUILayout.Button("Update",GUILayout.Height(30f)))
        {
            perkTree.SetRuneIMGs();
            Selection.activeGameObject = null;
        }
        GUILayout.TextArea("Set all runes to the img inputed in 'Rune IMG', but it isn't perfect you need to click on perk tree again for it to refresh and show new img's.");
    }
}