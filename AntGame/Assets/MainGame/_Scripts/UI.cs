using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnknownGames
{
    public class UI : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        private bool gameIsOver;

        #endregion

        #region PUBLIC VARIABLES

        public GameObject GameLoseUI;
        public GameObject GameWinUI;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            EnemyGuard.OnPlayerWasSpotted += ShowGameLoseUI;
            FindObjectOfType<PlayerMovement>().OnReachedExit += ShowGameWinUI;
        }

        private void Update()
        {
            if (gameIsOver) // put this later in the GameManager (GameManager.instance.gameOver = true or something)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }

        #endregion

        #region PRIVATE METHODS

        private void ShowGameWinUI()
        {
            OnGameOver(GameWinUI);
        }

        private void ShowGameLoseUI()
        {
            OnGameOver(GameLoseUI);
        }

        private void OnGameOver(GameObject panel)
        {
            panel.SetActive(true);
            gameIsOver = true;
            FindObjectOfType<PlayerMovement>().OnReachedExit -= ShowGameWinUI;
            EnemyGuard.OnPlayerWasSpotted -= ShowGameLoseUI;
        }

        #endregion

        #region PUBLIC METHODS

        // public methods here

        #endregion
    }
}