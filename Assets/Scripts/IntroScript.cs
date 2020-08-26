using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Intro
{
    public class IntroScript : MonoBehaviour
    {
        private BasicChar Player => PlayerMain.Player;

        [SerializeField] private Button firstAccept = null, secondAccept = null;

        [SerializeField] private TMP_InputField firstName = null, lastName = null;

        [SerializeField] private GameObject charCreator = null, startSettings = null;

        private string FirstName => firstName.text;
        private string LastName => lastName.text;


        private void Start()
        {
            firstAccept.onClick.AddListener(NamePlayer);
            secondAccept.onClick.AddListener(StartGame);
            charCreator.SetActive(true);
            startSettings.SetActive(false);
        }

        private void NamePlayer()
        {
            Player.Identity.SetFirstName(FirstName);
            Player.Identity.SetLastName(LastName);
        }

        private void StartGame()
        {
            GameManager.SetCurState(GameState.Free);
            Destroy(gameObject);
        }
    }
}