using System.Collections;
using UnityEngine;
using Pathfinding;

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

        private Rigidbody2D rigidbody2D;
        private Player player;

        #endregion

        #region PUBLIC VARIABLES

        public AIPath path;
        public AIDestinationSetter destinationSetter;
        public float Health;
        public float MoveSpeed;
        public bool stopped;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            PlayerGO = GameObject.FindGameObjectWithTag("Player");
            player = PlayerGO.GetComponent<Player>();
            path = GetComponent<AIPath>();
            destinationSetter = GetComponent<AIDestinationSetter>();

            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            playerTransform = PlayerGO.transform;
            stopped = false;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            rigidbody2D.velocity = Vector2.zero;
        }

        private void Update()
        {
            transform.up = playerTransform.position - transform.position;

            if (Input.GetKeyDown(KeyCode.Q) && !stopped)
            {
                player.mate.path.enabled = false;
                player.mate.stopped = true;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && stopped)
            {
                player.mate.path.enabled = true;
                player.mate.stopped = false;
            }
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

        // stay at current position
        // fight selected enemy
        // pick up item and:
        // go home
        // come back (faster)

        #endregion
    }
}