using UnityEngine;

public class TickManager : MonoBehaviour
{
    public playerMain player;
    private float _reGainRate;
    private void OnEnable()
    {
        InvokeRepeating("ReGain", 0f, 5f);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void ReGain()
    {
        player.Hp = 1f;
        player.Wp = 1f;
    }
}