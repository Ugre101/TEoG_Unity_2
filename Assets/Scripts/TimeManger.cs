using UnityEngine;

public class TimeManger : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start() => StartCoroutine(DateSystem.TickMinute());
}