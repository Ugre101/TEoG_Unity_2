using UnityEngine;

public class TickManager : MonoBehaviour
{
    private void OnEnable()
    {
        InvokeRepeating("ReGain", 0f, 1f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}