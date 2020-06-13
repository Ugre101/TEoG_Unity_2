using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class IntroScript : MonoBehaviour
    {
        private PlayerMain Player => PlayerHolder.Player;

        [SerializeField] private Button firstAccept = null, secondAccept = null;

        [SerializeField] private TMP_InputField firstName = null, lastName = null;

        [SerializeField] private GameObject charCreator = null, startSettings = null;

        private string FirstName => firstName.text;
        private string LastName => lastName.text;

        private void OnEnable() => GameManager.SetCurState(GameState.Intro);

        private void Start()
        {
            firstAccept.onClick.AddListener(NamePlayer);
            secondAccept.onClick.AddListener(StartGame);
            charCreator.SetActive(true);
            startSettings.SetActive(false);
        }

        private void NamePlayer()
        {
            Player.Identity.FirstName = FirstName;
            Player.Identity.LastName = LastName;
        }

        private void StartGame()
        {
            CanvasMain.GetCanvasMain.Resume();
            Destroy(gameObject);
        }
    }
}