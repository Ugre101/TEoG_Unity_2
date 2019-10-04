using UnityEngine;

public class TickManager : MonoBehaviour
{
    public playerMain player;
    public EventLog eventlog;
    public PerkInfo healtyBody, strongMind;
    private float _reGainRate;
    private int hour, day, month, year;

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
        player.HP.Gain(1f + healtyBody.Value);
        player.WP.Gain(1f + strongMind.Value);
        if (player.Balls.Count > 0)
        {
            foreach (Balls ball in player.Balls)
            {
                ball.Fluid.ReFill();
            }
        }
        if (player.Lactating)
        {
            foreach (Boobs boob in player.Boobs)
            {
                boob.Fluid.ReFill();
            }
        }
        eventlog.AddTo("tick");
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
}