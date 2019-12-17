using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class IntroScript : MonoBehaviour
    {
        [SerializeField]
        private CanvasMain gameUI;

        [SerializeField]
        private PlayerMain player;

        [SerializeField]
        private Button firstAccept, secondAccept;

        [SerializeField]
        private TMP_InputField firstName, lastName;

        [SerializeField]
        private GameObject charCreator, startSettings;

        public string FirstName => firstName.text;
        public string LastName => lastName.text;
        private void OnEnable()
        {
            GameManager.CurState = GameState.Intro;
        }
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
}