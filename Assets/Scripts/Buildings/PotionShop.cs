using TMPro;
using UnityEngine;

public class PotionShop : MonoBehaviour
{
    public PlayerMain player;
    public TextMeshProUGUI textbox;

    // Start is called before the first frame update
    private void Start()
    {
    }
}

public class Potion : Ware
{
    public Potion(int parCost, string parTitle, string parDesc)
    {
        Cost = parCost;
        Title = parTitle;
        Desc = parDesc;
    }
}