public class LoseSexButton : SexButtonBase
{
    private LoseMain loseMain;

    private void Func()
    {
        loseMain.AddToTextBox(scene.StartScene(player, other));
        loseMain.CanLeave();
    }

    public void Setup(PlayerMain playerMain, BasicChar basicChar, LoseMain loseMain, LoseScene loseScene)
    {
        this.player = playerMain;
        this.other = basicChar;
        this.loseMain = loseMain;
        this.scene = loseScene;
        btn.onClick.AddListener(Func);
        title.text = scene.name;
    }
}