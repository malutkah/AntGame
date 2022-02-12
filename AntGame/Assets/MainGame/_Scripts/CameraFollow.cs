using UnityEngine;

namespace UnknownGames
{
    public class CameraFollow : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here
        private Vector3 offset;

        private Vector3 targetPos;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float LerpSpeed;
        public Transform target;

        #endregion

        #region UNITY METHODS
        
        private void Start()
        {
            if (target == null) return;

            offset = transform.position - target.position;
        }

        private void Update()
        {
            if (target == null) return;

            targetPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPos, LerpSpeed * Time.deltaTime);
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