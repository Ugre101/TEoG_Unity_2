using UnityEditor;

[CustomEditor(typeof(PlayerMain))]
public class PlayerMainEditor : BasicCharEditor
{
    private PlayerMain player;

    public override void OnInspectorGUI()
    {
        player = (PlayerMain)target;
        base.OnInspectorGUI();
    }
}