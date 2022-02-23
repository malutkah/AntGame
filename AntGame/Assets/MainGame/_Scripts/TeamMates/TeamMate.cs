using UnityEngine;

namespace UnknownGames
{
    public class TeamMate : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here
        private int carryCapacity = 1;
        private Transform playerTransform;

        [SerializeField]
        private float distanceToPlayer;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float Health;
        public float MoveSpeed;
        public GameObject PlayerGO;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            playerTransform = PlayerGO.transform;
        }

        private void Update()
        {
            FollowPlayer();
        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

        #endregion

        #region PUBLIC METHODS

        // follow player
        public void FollowPlayer()
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > distanceToPlayer)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    playerTransform.position, MoveSpeed * Time.deltaTime);
            }
        }

        // stay at current position
        // fight selected enemy
        // pick up item and:
        // go home
        // come back (faster)

        #endregion
    }
}