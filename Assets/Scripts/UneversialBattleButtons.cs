using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UneversialBattleButtons : MonoBehaviour
{
    //public PlayerChar player;
    //public EnemyChar enemy;
    public GameObject combatPanel, sexPanel, losePanel;

    // Private
    private Scene scene;

    // Start is called before the first frame update
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void LeaveBattle()
    {
        combatPanel.SetActive(true);
        sexPanel.SetActive(false);
        losePanel.SetActive(false);
        SceneManager.LoadScene("MainGame");
    }
}