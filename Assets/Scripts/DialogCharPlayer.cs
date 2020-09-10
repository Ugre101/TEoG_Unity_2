public class DialogCharPlayer : DialogChar
{
    private void OnEnable()
    {
        whom = whom ?? PlayerMain.Player;
        SetTexts(whom);
    }
}