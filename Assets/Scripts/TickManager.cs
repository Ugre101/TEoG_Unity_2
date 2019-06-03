using UnityEngine;

public class TickManager : MonoBehaviour
{
    public playerMain player;
    private float _reGainRate;
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
        player.HP.Gain(1f);
        player.WP.Gain(1f);
        if (player.Balls.Count > 0)
        {
            foreach(Balls ball in player.Balls)
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
    }
}