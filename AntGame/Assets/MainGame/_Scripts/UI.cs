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
            MissionManager.OnPlayerGotSpotted += ShowGameLoseUI;
            MissionManager.OnPlayerReachedGoal += ShowGameWinUI;
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

        private void OnGameOver(GameObject panel)
        {
            panel.SetActive(true);
            gameIsOver = true;
            MissionManager.OnPlayerReachedGoal -= ShowGameWinUI;
            MissionManager.OnPlayerGotSpotted -= ShowGameLoseUI;
        }

        #endregion

        #region PUBLIC METHODS

        public void ShowGameWinUI()
        {
            OnGameOver(GameWinUI);
        }

        public void ShowGameLoseUI()
        {
            OnGameOver(GameLoseUI);
        }

        #endregion
    }
}