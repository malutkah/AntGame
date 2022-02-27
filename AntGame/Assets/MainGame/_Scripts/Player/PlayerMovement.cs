using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnknownGames
{
    public class PlayerMovement : MonoBehaviour
    {
        //public event System.Action OnReachedExit;

        #region PRIVATE VARIABLES

        // private variables here
        private Vector3 moveDelta;
        private RaycastHit2D hit;
        private Rigidbody2D rigidbody2d;
        private Camera viewCamera;
        private Vector2 moveDir;
        private bool disableMovement;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float speed;
        public BoxCollider2D boxCollider;

        #endregion

        #region UNITY METHODS

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Finish")
            {
                Disable();

                MissionManager.instance.PlayerReachedGoal = true;
            }
        }

        private void Awake()
        {
            viewCamera = Camera.main;
        }

        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();

            MissionManager.OnPlayerGotSpotted += Disable;
        }

        private void OnDestroy()
        {
            MissionManager.OnPlayerGotSpotted -= Disable;
        }

        private void Update()
        {
            moveDir = Vector2.zero;
            var dir = Input.mousePosition - viewCamera.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            #region Movement

            moveDir = Vector2.zero;

            if (!disableMovement)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    moveDir.x = -1;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveDir.x = 1;
                }
                if (Input.GetKey(KeyCode.W))
                {
                    moveDir.y = 1;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveDir.y = -1;
                }
            }

            moveDir.Normalize();

            #endregion
        }

        private void FixedUpdate()
        {
            rigidbody2d.MovePosition(rigidbody2d.position + moveDir * speed * Time.fixedDeltaTime);
        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

        private void Disable()
        {
            disableMovement = true;
        }

        private void CollideWithTiles()
        {
            //collision detection y axis
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Characters", "Blocking"));
            if (hit.collider == null)
            {
                //Movement
                transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
            }

            //collision detection x axis
            hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Characters", "Blocking"));
            if (hit.collider == null)
            {
                //Movement
                transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
            }
        }

        #endregion

        #region PUBLIC METHODS

        // public methods here

        #endregion
    }
}