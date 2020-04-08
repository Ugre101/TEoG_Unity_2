using UnityEditor;

[CustomEditor(typeof(PlayerMain))]
public class PlayerMainEditor : BasicCharEditor
{
    private PlayerMain player;

    private void OnEnable()
    {
        BasicCharEnable();
        player = (PlayerMain)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}