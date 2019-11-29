using UnityEditor;

[CustomEditor(typeof(playerMain))]
public class PlayerMainEditor : BasicCharEditor
{
    private playerMain player;

    public override void OnInspectorGUI()
    {
        player = (playerMain)target;
        base.OnInspectorGUI();
    }
}