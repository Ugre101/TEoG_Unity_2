using UnityEngine;

public class GameButtonHandler : MonoBehaviour
{
    public GameObject Vore, LevelUp;
    public playerMain player;

    // OnEnable should be perfect?
    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("voreToggle"))
        {
            Vore.SetActive(PlayerPrefs.GetInt("voreToggle") == 1 ? true : false);
        }
        else
        {
            Vore.SetActive(false);
        }
    }

    private void Update()
    {
        if (LevelUp != null)
        {
            if (player.StatsPoints > 0 || player.PerkPoints > 0)
            {
                LevelUp.SetActive(true);
            }
            else if (LevelUp.activeSelf)
            {
                LevelUp.SetActive(false);
            }
        }
    }
}