using UnityEngine;

namespace UnknownGames
{
    public class PlayerMovement : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here
        private Vector3 moveDelta;
        private RaycastHit2D hit;
        private Rigidbody2D rigidbody2d;
        private Camera viewCamera;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float speed;
        public BoxCollider2D boxCollider;

        #endregion

        #region UNITY METHODS

        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            viewCamera = Camera.main;
        }

        private void Update()
        {
            var dir = Input.mousePosition - viewCamera.WorldToScreenPoint(transform.position);
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            CollideWithTiles();

            #region Movement
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            //MoveDelta reset
            moveDelta = new Vector3(x, y, 0) * Time.deltaTime * speed;

            transform.localScale = Vector3.one;
            #endregion
        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

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