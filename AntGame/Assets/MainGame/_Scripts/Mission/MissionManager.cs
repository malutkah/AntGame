using UnityEngine;

namespace UnknownGames
{
    public class MissionManager : MonoBehaviour
    {
        #region EVENTS

        public static event System.Action OnPlayerReachedGoal;
        public static event System.Action OnPlayerGotSpotted;

        #endregion

        #region PRIVATE VARIABLES

        // private variables here

        #endregion

        #region PUBLIC VARIABLES

        public static MissionManager instance;

        [HideInInspector]
        public bool WasPlayerSpotted;
        [HideInInspector]
        public bool PlayerReachedGoal;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (WasPlayerSpotted)
            {
                if (OnPlayerGotSpotted != null)
                {
                    OnPlayerGotSpotted();
                }
            }

            if (PlayerReachedGoal)
            {
                if (OnPlayerReachedGoal != null)
                {
                    OnPlayerReachedGoal();
                }
            }
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