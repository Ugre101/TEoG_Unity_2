using UnityEditor;

[CustomEditor(typeof(PlayerHolder))]
public class PlayerHolderEditor : CharHolderEditor
{
    
    private PlayerHolder playerHolder;

    private PlayerMain Player
    {
        get
        {
            if (playerHolder.BasicChar is PlayerMain p)
            {
                return p;
            }
            return null;
        }
    }
    private void OnEnable()
    {
        playerHolder = (PlayerHolder)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
    
}
