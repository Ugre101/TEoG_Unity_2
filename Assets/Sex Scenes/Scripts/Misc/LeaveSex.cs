public class LeaveSex : MonoScene
{
    private GameUI gameUI;
    public void Start()
    {
        gameUI = GetComponentInParent<GameUI>();
    }
    public override void DoScene(playerMain player, BasicChar other)
    {
        gameUI.Resume();
    }
}
