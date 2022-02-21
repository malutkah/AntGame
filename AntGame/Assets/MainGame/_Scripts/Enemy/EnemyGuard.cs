using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace UnknownGames
{
    public class EnemyGuard : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here
        private float viewAngle;
        private Transform player;
        private Color originalSpotLightColor;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public Transform pathHolder;
        public float MoveSpeed = 5f;
        public float WaitTimeInSec = 1f;
        public float TurnSpeed = 90f;
        public LayerMask ViewMask;
        public Light2D SpotLight;

        public float ViewDistance;

        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Start()
        {
            viewAngle = SpotLight.pointLightInnerAngle;
            originalSpotLightColor = SpotLight.color;

            Vector3[] waypoint = new Vector3[pathHolder.childCount];

            for (int k = 0; k < waypoint.Length; k++)
            {
                waypoint[k] = pathHolder.GetChild(k).position;
            }

            StartCoroutine(FollowPath(waypoint));
        }

        private void Update()
        {
            if (CanSeePlayer())
            {
                SpotLight.color = Color.red;
            }
            else
            {
                SpotLight.color = originalSpotLightColor;
            }
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

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.position * ViewDistance);

        }

        #endregion

        #region PRIVATE METHODS

        // private methods here

        private bool CanSeePlayer()
        {
            if ((Vector3.Distance(transform.position, player.position) / 10f) < (ViewDistance * 10f))
            {
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                //new Vector3(0, transform.position.y, transform.position.z)
                float angleBetweenGuardAndPlayer = Vector3.Angle(new Vector3(0, transform.position.y, transform.position.z), directionToPlayer);

                if (angleBetweenGuardAndPlayer < viewAngle / 2f)
                {
                    if (Physics.Linecast(transform.position, player.position, ViewMask))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private IEnumerator FaceNextWaypoint(Vector3 target)
        {
            Vector3 directonToFaceTarget = (target - transform.position).normalized;
            float targetAngle = 270 + Mathf.Atan2(directonToFaceTarget.y, directonToFaceTarget.x) * Mathf.Rad2Deg;

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

                if (transform.position == new Vector3(targetWaypoint.x, targetWaypoint.y, 0))
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