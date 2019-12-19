using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PotionShop : MonoBehaviour
{
    public PlayerMain player;
    public TextMeshProUGUI textbox;
    public Button str, charm, end,dex, inte;
    // Start is called before the first frame update
    void Start()
    {
        if (str != null)
        {
            str.onClick.AddListener(BuyStr);
        }
        if (charm != null)
        {
            charm.onClick.AddListener(BuyCharm);
        }
        if (end != null)
        {
            end.onClick.AddListener(BuyEnd);
        }
        if (inte != null)
        {
            inte.onClick.AddListener(BuyInt);
        }
        if (dex != null)
        {
            dex.onClick.AddListener(BuyDex);
        }
    }

   private bool BaseBuy(int cost = 100)
    {
        if (player.Currency.TryToBuy(cost))
        {
            return true;
        }else
        {
            textbox.text = "Sorry you can't afford that";
            return false;
        }
    }
    private void BuyStr()
    {
        if (BaseBuy(100))
        {
            textbox.text = "afford str";
            player.Stats.Strength.BaseValue++;
        }
    }

    private void BuyCharm()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
            player.Stats.Charm.BaseValue++;
        }
    }

    private void BuyEnd()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
            player.Stats.Endurance.BaseValue++;
        }
    }
    private void BuyDex()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
            player.Stats.Dexterity.BaseValue++;
        }
    }
    private void BuyInt()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
          // Add int to player
        }
    }
}
