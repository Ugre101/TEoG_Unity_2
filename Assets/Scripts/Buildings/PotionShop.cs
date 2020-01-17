using TMPro;

public class PotionShop : Building
{
    public TextMeshProUGUI textbox;
}

public class Potion : Ware
{
    public Potion(int parCost, string parTitle, string parDesc) : base(parCost, parTitle, parDesc)
    {
    }
}