using UnityEngine;

namespace Intro
{
    public class IntroCanvas : MonoBehaviour
    {
        [SerializeField] private IntroScript introScript = null;

        // Start is called before the first frame update
        private void Awake()
        {
            GameManager.GameStateChangeEvent += StartIntroOrDestroy;
        }

        private void StartIntroOrDestroy(GameState newState)
        {
            if (newState == GameState.Intro)
                introScript.gameObject.SetActive(true);
            else
            {
                GameManager.GameStateChangeEvent -= StartIntroOrDestroy;
                Destroy(gameObject);
            }
        }
    }
}