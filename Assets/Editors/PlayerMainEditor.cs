using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(playerMain))]
public class PlayerMainEditor : Editor
{
    private playerMain player;

    public override void OnInspectorGUI()
    {
        player = (playerMain)target;
        GUILayout.Label("Summary", EditorStyles.boldLabel);
        GUILayout.BeginVertical("Box");
        GUILayout.Label(player.FullName);
        GUILayout.BeginHorizontal();
        GUILayout.Label(player.Gender.ToString());
        GUILayout.Label(player.Race);
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        base.OnInspectorGUI();
    }
}