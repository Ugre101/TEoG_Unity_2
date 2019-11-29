using UnityEngine;

public class TickManager : MonoBehaviour
{
    public playerMain player;
    public EventLog eventlog;
    public PerkInfo healtyBody, strongMind, gluttony, lowMetabolism;

    [SerializeField]
    private float baseRecGainRate = 1f;

    [SerializeField]
    private float baseFatBurnRate = 0.0005f;

    private int minute = 0, hour, day, month, year;

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
        player.Body.Fat.Lose(fatBurnRate);

        player.HP.Gain(hpGain);
        player.WP.Gain(wpGain);
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
        if (minute++ > 60)
        {
            DateSystem();
            minute = 0;
        }
    }

    private void DateSystem()
    {
        hour++;
        if (hour > 24)
        {
            hour = 1;
            day++;
        }
        if (day > 30)
        {
            day = 1;
            month++;
        }
        if (month > 12)
        {
            month = 1;
            year++;
        }
    }

    private string CurrentDate()
    {
        return $"{year} {month} {day} {hour}";
    }

    public void Sleep()
    {
        // loop so I can add stuff inside datesystem later, like e.g. dorm stuff.
        for (int i = 0; i < 8; i++)
        {
            DateSystem();
        }
        player.HP.FullGain();
        player.WP.FullGain();
    }

    public DateSave Save()
    {
        return new DateSave(year, month, day, hour);
    }
}