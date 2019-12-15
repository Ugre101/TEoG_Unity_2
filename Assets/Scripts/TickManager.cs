using UnityEngine;

public class TickManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMain player = null;

    [SerializeField]
    private PerkInfo healtyBody = null, strongMind = null, gluttony = null, lowMetabolism = null;

    [SerializeField]
    private float baseRecGainRate = 1f;

    [SerializeField]
    private float baseFatBurnRate = 0.0005f;

    private int minute = 0;

    private void OnEnable()
    {
        InvokeRepeating("ReGain", 0f, 1f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void ReGain()
    {
        float fatBurnRate = baseFatBurnRate + gluttony.PosetiveValue;
        float hpGain = baseRecGainRate + healtyBody.PosetiveValue;
        float wpGain = baseRecGainRate + strongMind.PosetiveValue;
        if (player.Perks.HasPerk(PerksTypes.Gluttony))
        {
            fatBurnRate += gluttony.NegativeValue;
            hpGain += gluttony.PosetiveValue;
            wpGain += gluttony.PosetiveValue;
        }
        else if (player.Perks.HasPerk(PerksTypes.LowMetabolism))
        {
            fatBurnRate -= lowMetabolism.PosetiveValue;
        }
        player.Body.Fat.LoseFlat(fatBurnRate);
        player.HP.Gain(hpGain);
        player.WP.Gain(wpGain);
        ReGainFluids();
        if (minute++ > 60)
        {
            minute = 0;
            DateSystem.PassHour();
        }
        EventLog.AddTo("Tick");
    }

    public void ReGainFluids()
    {
        if (player.SexualOrgans.Balls.Count > 0)
        {
            foreach (Balls ball in player.SexualOrgans.Balls)
            {
                ball.Fluid.ReFill();
            }
        }
        if (player.SexualOrgans.Lactating)
        {
            foreach (Boobs boob in player.SexualOrgans.Boobs)
            {
                boob.Fluid.ReFill();
            }
        }
    }
}