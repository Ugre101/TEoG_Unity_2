using UnityEditor;

// TODO understand why this chrases editor on runtime
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