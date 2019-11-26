using UnityEngine;

public class GameButtonHandler : MonoBehaviour
{
    public GameObject Vore, LevelUp;
    public playerMain player;
    private bool levelNotNull = false, voreNotNull = false, hasLeveld = false;

    private void OnEnable()
    {
        /*   if (PlayerPrefs.HasKey("voreToggle"))
           {
               Vore.SetActive(PlayerPrefs.GetInt("voreToggle") == 1 ? true : false);
           }
           else
           {
               Vore.SetActive(false);
           } */
        levelNotNull = LevelUp != null;
        voreNotNull = Vore != null;
        hasLeveld = player.ExpSystem.PerkPoints > 0;
        if (levelNotNull)
        {
            LevelUp.SetActive(hasLeveld);
        }
        if (voreNotNull)
        {
            Vore.SetActive(player.Vore.Active);
        }
    }
}