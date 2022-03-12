using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Pathfinding;

namespace UnknownGames
{
    public class Player : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        private float hp;
        private float stealth;
        private float damage;
        private float speed;
        private float power;
        private int MinTeamSize = 0;

        #endregion

        #region PUBLIC VARIABLES

        public List<GameObject> TeamList;
        public int MaxTeamSize = 2;
        public GameObject SelectedMate;
        [HideInInspector] public TeamMate mate;


        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            //DontDestroyOnLoad(gameObject); => do DontDestroy befor changing scenes
        }

        private void Start()
        {
            // add team mates to list
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                // select first mate from team list
                SelectedMate = TeamList[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // select second mate from team list
                SelectedMate = TeamList[1];
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // select all mate from team list
                SelectedMate = TeamList[1];
            }

            if (SelectedMate != null)
            {
                mate = SelectedMate.GetComponent<TeamMate>();
            }
        }

        #endregion

        #region PRIVATE METHODS        


        #endregion

        #region PUBLIC METHODS

        public void MissionStart()
        {
            MissionManager.instance.TeamSize = TeamList.Count;
        }

        public void FillTeam(GameObject teamMate)
        {
            if (TeamList.Count <= MaxTeamSize)
            {
                TeamList.Add(teamMate);
            }
            else
            {
                Debug.Log("Team is full");
            }
        }

        #endregion
    }
}