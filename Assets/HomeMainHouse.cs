using UnityEngine;

public class HomeMainHouse : MonoBehaviour
{
    [SerializeField] private PlayerMain player = null;
    private int Level => StartHomeStats.MainHouse.Level;

    public void Start()
    {
        player = player != null ? player : PlayerMain.GetPlayer;
    }

    public void Sleep()
    {
        if (Level == 0)
        {
            DateSystem.PassHour(8);
            // Heal should take care of itself
        }
    }
}