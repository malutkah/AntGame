using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnknownGames;

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

        public List<GameObject> Team;
        public int MaxTeamSize = 2;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            //DontDestroyOnLoad(gameObject); => do DontDestroy befor changing scenes
        }

        private void Start()
        {
            // testing
            // add team mates to list
        }

        private void Update()
        {
        }

        #endregion

        #region PRIVATE METHODS        


        #endregion

        #region PUBLIC METHODS
        
        public void MissionStart()
        {
            MissionManager.instance.TeamSize = Team.Count;
        }

        public void FillTeam(GameObject teamMate)
        {
            if (Team.Count <= MaxTeamSize)
            {
                Team.Add(teamMate);
            }
            else
            {
                Debug.Log("Team is full");
            }
        }

        #endregion
    }
}