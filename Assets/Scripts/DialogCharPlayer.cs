public class DialogCharPlayer : DialogChar
{
    private void OnEnable()
    {
        whom = whom != null ? whom : PlayerHolder.Player;
        SetTexts(whom);
    }
}