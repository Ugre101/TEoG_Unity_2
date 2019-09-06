using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    public playerMain player;
    public Button rest, small, medium, large;

    // Start is called before the first frame update
    private void Start()
    {
        rest.onClick.AddListener(Rest);
        small.onClick.AddListener(Small);
        medium.onClick.AddListener(Medium);
        large.onClick.AddListener(Large);
    }

    private void Rest()
    {
        if (player.CanAfford(5))
        {
            player.HP.FullGain();
            player.WP.FullGain();
        }
    }

    private void Small()
    {
        if (player.CanAfford(3))
        {
            player.HP.Gain(3);
            player.WP.Gain(3);
            player.Weight += 3;
        }
    }

    private void Medium()
    {
        if (player.CanAfford(5))
        {
            player.HP.Gain(5);
            player.WP.Gain(5);
            player.Weight += 5;
        }
    }

    private void Large()
    {
        if (player.CanAfford(8))
        {
            player.HP.Gain(8);
            player.WP.Gain(8);
            player.Weight += 8;
        }
    }
}