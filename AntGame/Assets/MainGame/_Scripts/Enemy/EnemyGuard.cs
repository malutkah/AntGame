using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace UnknownGames
{
    public class EnemyGuard : MonoBehaviour
    {
        //public static event System.Action OnPlayerWasSpotted;

        #region PRIVATE VARIABLES

        // private variables here
        private float viewAngle;
        private float playerVisibleTimer;
        private GameObject player;
        private Color originalSpotLightColor;
        private List<GameObject> teamMates;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public Transform pathHolder;

        public float MoveSpeed = 5f;
        public float WaitTimeInSec = 1f;
        public float TurnSpeed = 90f;
        public float ViewDistance;
        public float TimeToSpotPlayer = 1f;

        public LayerMask ViewMask;
        public Light2D SpotLight;


        #endregion

        #region UNITY METHODS

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            teamMates = player.GetComponent<Player>().Team;
        }

        private void Start()
        {
            viewAngle = SpotLight.pointLightOuterAngle;
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
            if (CanSeePlayer() || CanSeeTeamMates())
            {
                playerVisibleTimer += Time.deltaTime;
            }
            else
            {
                playerVisibleTimer -= Time.deltaTime;
            }

            playerVisibleTimer = Mathf.Clamp(playerVisibleTimer, 0, TimeToSpotPlayer);
            SpotLight.color = Color.Lerp(originalSpotLightColor, Color.red, playerVisibleTimer / TimeToSpotPlayer);

            if (playerVisibleTimer >= TimeToSpotPlayer)
            {
                // player got spotted
                MissionManager.instance.WasPlayerSpotted = true;
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
            Gizmos.DrawRay(transform.position, transform.up * ViewDistance);
        }

        #endregion

        #region PRIVATE METHODS

        private bool CanSeePlayer()
        {
            if (Vector3.Distance(transform.position, player.transform.position) < ViewDistance)
            {
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

                //new Vector3(0, transform.position.y, transform.position.z)
                float angleBetweenGuardAndPlayer = Vector3.Angle(transform.up, directionToPlayer);

                if (angleBetweenGuardAndPlayer < viewAngle / 2f)
                {
                    if (!Physics.Linecast(transform.position, player.transform.position, ViewMask))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanSeeTeamMates()
        {
            foreach (var mate in teamMates)
            {
                if (Vector3.Distance(transform.position, mate.transform.position) < ViewDistance)
                {
                    Vector3 directionToMate = (mate.transform.position - transform.position).normalized;

                    //new Vector3(0, transform.position.y, transform.position.z)
                    float angleBetweenGuardAndMate = Vector3.Angle(transform.up, directionToMate);

                    if (angleBetweenGuardAndMate < viewAngle / 2f)
                    {
                        if (!Physics.Linecast(transform.position, mate.transform.position, ViewMask))
                        {
                            return true;
                        }
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