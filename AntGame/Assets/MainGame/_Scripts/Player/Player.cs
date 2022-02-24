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

        #endregion

        #region PUBLIC VARIABLES

        public List<GameObject> Team;
        public int MaxTeamSize;
        public int MinTeamSize;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
        }

        private void Update()
        {
        }

        #endregion

        #region PRIVATE METHODS        


        #endregion

        #region PUBLIC METHODS

        public void FillTeam(GameObject teamMate)
        {
            Team.Add(teamMate);
        }

        #endregion
    }
}