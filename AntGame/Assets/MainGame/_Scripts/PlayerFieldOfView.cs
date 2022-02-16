using UnityEngine;

namespace UnknownGames
{
    public class PlayerFieldOfView : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float ViewRadius;

        [Range(0, 360)]
        public float ViewAngle;

        #endregion

        #region UNITY METHODS     
        #endregion

        #region PRIVATE METHODS
        // private methods here
        #endregion

        #region PUBLIC METHODS
        // public methods here
        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.z;
            }

            return new Vector3(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
        }
        #endregion
    }
}