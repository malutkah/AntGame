using System.Collections;
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

        [SerializeField]
        private float distanceToMate;

        private GameObject PlayerGO;

        private float hp;
        private float stealth;
        private float damage;
        private float speed;
        private float power;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float Health;
        public float MoveSpeed;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            PlayerGO = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            playerTransform = PlayerGO.transform;
        }

        private void Update()
        {
            FollowPlayer();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, transform.up * distanceToMate);
        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

        #endregion

        #region PUBLIC METHODS

        // follow player
        public void FollowPlayer()
        {
            if (Vector2.Distance(transform.position, playerTransform.position) > distanceToPlayer + distanceToMate)
            {
                Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;

                // face the player
                float targetAngle = 270 + Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

                while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, directionToPlayer.x)) > 0.05f)
                {
                    float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, 100 * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, 0, angle);

                    transform.position = Vector2.MoveTowards(transform.position,
                        playerTransform.position, MoveSpeed * Time.deltaTime);
                }
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