public class DialogCharPlayer : DialogChar
{
    private void OnEnable()
    {
        whom = whom != null ? whom : PlayerMain.Player;
        SetTexts(whom);
    }
}