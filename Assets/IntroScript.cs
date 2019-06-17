using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntroScript : MonoBehaviour
{
    public playerMain player;
    public GameUI gameUI;
    public Button accept, start;
    public TMP_InputField firstName, lastName;

    public GameObject Intro, CharCreator, StartSettings;
    // Start is called before the first frame update
    void Start()
    {
        accept.onClick.AddListener(AcceptName);
        start.onClick.AddListener(StartGame);
    }
    private void AcceptName()
    {
        player.firstName = firstName.text;
        player.lastName = lastName.text;
        CharCreator.SetActive(false);
        StartSettings.SetActive(true);
    }
    private void StartGame()
    {
        gameUI.Resume();
    }
}
