using System.Collections.Generic;
using UnityEngine;

namespace UnknownGames
{
    public class MissionManager : MonoBehaviour
    {
        #region EVENTS

        public static event System.Action OnPlayerReachedGoal;
        public static event System.Action OnPlayerGotSpotted;
        public static event System.Action OnMissionStart;

        #endregion

        #region PRIVATE VARIABLES

        // private variables here
        private GameUI ui;
        private GameObject teamPanel;
        private GameObject teamBox;

        private List<GameObject> teamMatesUi;

        #endregion

        #region PUBLIC VARIABLES

        public static MissionManager instance;

        [HideInInspector]
        public bool WasPlayerSpotted;
        [HideInInspector]
        public bool PlayerReachedGoal;
        [HideInInspector]
        public int TeamSize;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            ui = GameObject.FindGameObjectWithTag("MissionUI").GetComponent<GameUI>();
            //teamPanel = GameObject.FindGameObjectWithTag("Team");
            //teamBox = teamPanel.transform.GetChild(0).gameObject;
        }

        private void Start()
        {
            //for (int c = 0; c < TeamSize; c++)
            //{
            //    teamMatesUi.Add(teamBox.transform.GetChild(c).gameObject);
            //}
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