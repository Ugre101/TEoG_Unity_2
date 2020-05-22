using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Npc))]

public class NpcEditor : BasicCharEditor
{
    private void OnEnable()
    {
        BasicCharEnable();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}
