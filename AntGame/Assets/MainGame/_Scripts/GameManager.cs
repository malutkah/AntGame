using UnityEngine;

namespace UnknownGames
{
    public class GameManager : MonoBehaviour
    {


        #region PRIVATE VARIABLES

        // private variables here

        #endregion

        #region PUBLIC VARIABLES

        public GameManager instance;
        
        public bool IsGameOver;
        public GameUI Ui;


        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            if (instance != null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {

        }

        private void Update()
        {
            
        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

        #endregion

        #region PUBLIC METHODS

        // public methods here

        #endregion
    }
}