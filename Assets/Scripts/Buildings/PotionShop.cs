using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PotionShop : MonoBehaviour
{
    public playerMain player;
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
        if (player.CanAfford(cost))
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
            player.Stats.strength._baseValue++;
        }
    }

    private void BuyCharm()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
            player.Stats.charm._baseValue++;
        }
    }

    private void BuyEnd()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
            player.Stats.endurance._baseValue++;
        }
    }
    private void BuyDex()
    {
        if (BaseBuy(100))
        {
            textbox.text = "";
            player.Stats.dexterity._baseValue++;
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
