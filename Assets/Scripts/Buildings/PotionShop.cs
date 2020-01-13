using TMPro;

public class PotionShop : Building
{
    public TextMeshProUGUI textbox;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
}

public class Potion : Ware
{
    public Potion(int parCost, string parTitle, string parDesc) : base(parCost, parTitle, parDesc)
    {
    }
}