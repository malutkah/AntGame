using System.Collections;
using UnityEngine;

namespace UnknownGames
{
    public class EnemyGuard : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public Transform pathHolder;
        public float MoveSpeed = 5f;
        public float WaitTimeInSec = 1f;
        public float TurnSpeed = 90f;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
        }

        private void Start()
        {
            Vector3[] waypoint = new Vector3[pathHolder.childCount];

            for (int k = 0; k < waypoint.Length; k++)
            {
                waypoint[k] = pathHolder.GetChild(k).position;
            }

            StartCoroutine(FollowPath(waypoint));
        }

        private void Update()
        {

        }

        private void OnDrawGizmos()
        {
            Vector3 startPos = pathHolder.GetChild(0).position;
            Vector3 prevPos = startPos;

            foreach (Transform waypoint in pathHolder)
            {
                Gizmos.DrawSphere(waypoint.position, .3f);
                Gizmos.DrawLine(prevPos, waypoint.position);
                prevPos = waypoint.position;
            }

        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

        private IEnumerator FaceNextWaypoint(Vector3 target)
        {
            Vector3 directonToFaceTarget = (target - transform.position).normalized;
            float targetAngle = 90 + Mathf.Atan2(directonToFaceTarget.y, directonToFaceTarget.x) * Mathf.Rad2Deg;

            while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle)) > 0.05f)
            {
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, TurnSpeed * Time.deltaTime);
                transform.eulerAngles = new Vector3(0, 0, angle);

                yield return null;
            }
        }

        IEnumerator FollowPath(Vector3[] waypoints)
        {
            transform.position = waypoints[0]; 

            int targetWaypointIndex = 1;
            bool reachedLastWaypoint = false;
            Vector3 targetWaypoint = waypoints[targetWaypointIndex];
            // new Vector3(transform.position.x, transform.position.y, targetWaypoint.z)
            //transform.LookAt(targetWaypoint);
            //Vector2 dir = targetWaypoint - transform.position;
            //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            while (true)
            {
                transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(targetWaypoint.x, targetWaypoint.y), MoveSpeed * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, MoveSpeed * Time.deltaTime);

                if (transform.position == targetWaypoint)
                {
                    if (!reachedLastWaypoint)
                    {
                        targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;

                        if ((targetWaypointIndex + 1) % waypoints.Length == 0)
                        {
                            reachedLastWaypoint = true;
                            targetWaypointIndex = waypoints.Length - 1;
                        }
                    }
                    else
                    {
                        targetWaypointIndex = (targetWaypointIndex - 1) % waypoints.Length;

                        if (targetWaypointIndex == 0)
                        {
                            reachedLastWaypoint = false;
                        }
                    }

                    targetWaypoint = waypoints[targetWaypointIndex];

                    yield return new WaitForSeconds(WaitTimeInSec);
                    yield return StartCoroutine(FaceNextWaypoint(targetWaypoint));
                }

                yield return null;
            }
        }

        #endregion

        #region PUBLIC METHODS

        // public methods here

        #endregion
    }
}