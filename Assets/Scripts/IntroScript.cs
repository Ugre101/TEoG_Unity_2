using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public CanvasMain gameUI;
    public PlayerMain player;
    public Button firstAccept, secondAccept;
    public TMP_InputField firstName, lastName;
    public GameObject charCreator, startSettings;
    public string FirstName => firstName.text;
    public string LastName => lastName.text;

    private void Start()
    {
        firstAccept.onClick.AddListener(NamePlayer);
        secondAccept.onClick.AddListener(StartGame);
        charCreator.SetActive(true);
        startSettings.SetActive(false);
    }

    private void NamePlayer()
    {
        player.firstName = FirstName;
        player.lastName = LastName;
    }

    private void StartGame()
    {
        gameUI.Resume();
        Destroy(gameObject);
    }
}