using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnknownGames
{
    public class PlayerFieldOfView : MonoBehaviour
    {
        #region PRIVATE VARIABLES

        // private variables here
        private Mesh viewMesh;

        #endregion

        #region PUBLIC VARIABLES

        // public variables here
        public float ViewRadius;

        [Range(0, 360)]
        public float ViewAngle;

        public LayerMask TargetMask;
        public LayerMask ObstacleMask;

        public List<Transform> VisibleTargets = new List<Transform>();

        public float MeshResolution;
        public MeshFilter ViewMeshFilter;

        #endregion

        #region UNITY METHODS     

        private void Start()
        {
            viewMesh = new Mesh();
            viewMesh.name = "View Mesh";
            ViewMeshFilter.mesh = viewMesh;

            StartCoroutine("FindTargetWithDelay", .2f);
        }

        private void LateUpdate()
        {
            DrawFoV();
        }

        #endregion

        #region PRIVATE METHODS
        // private methods here

        private void DrawFoV()
        {
            int stepCount = Mathf.RoundToInt(ViewAngle * MeshResolution);

            float stepAngleSize = ViewAngle / stepCount;

            List<Vector2> viewPoints = new List<Vector2>();

            for (int i = 0; i <= stepCount; i++)
            {
                float angle = transform.eulerAngles.y - ViewAngle / 2 + stepAngleSize * i;
                //Debug.DrawLine(transform.position, transform.position + DirectionFromAngle(angle, false) * ViewRadius, Color.blue);

                ViewCastInfo info = ViewCast(angle);
                viewPoints.Add(info.point);
            }

            int vertexCount = viewPoints.Count + 1;

            Vector2[] vertices = new Vector2[vertexCount];
            int[] triangles = new int[(vertexCount - 2) * 3];

            vertices[0] = Vector2.zero;

            for (int i = 0; i < vertexCount - 1; i++)
            {
                vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

                if (i < vertexCount - 2)
                {
                    triangles[i * 3] = 0;
                    triangles[i * 3 + 1] = i + 1;
                    triangles[i * 3 + 2] = i + 2;
                }
            }

            Vector3[] vertices3D = System.Array.ConvertAll<Vector2, Vector3>(vertices, v => v);

            viewMesh.Clear();
            viewMesh.vertices = vertices3D;
            viewMesh.triangles = triangles;
            viewMesh.RecalculateNormals();

        }

        private IEnumerator FindTargetWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTargets();
            }
        }

        private void FindVisibleTargets()
        {
            VisibleTargets.Clear();
            Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), ViewRadius, TargetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                var target = targetsInViewRadius[i].transform;

                // die strecke zwischen player pos und target pos
                Vector2 directionToTarget = (target.position - transform.position).normalized;
                Vector2 pos1 = new Vector2(transform.up.x, transform.up.y);

                if (Vector2.Angle(pos1, directionToTarget) < ViewAngle / 2)
                {
                    float distanceToTarget = Vector2.Distance(new Vector2(transform.position.x, transform.position.y), new Vector2(target.position.x, target.position.y));

                    if (!Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(directionToTarget.x, directionToTarget.y), distanceToTarget, ObstacleMask))
                    {
                        VisibleTargets.Add(target);
                    }
                }
            }
        }

        private ViewCastInfo ViewCast(float globalAngle)
        {
            Vector2 dir = DirectionFromAngle(globalAngle);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), new Vector2(dir.x, dir.y), ViewRadius, ObstacleMask);

            //RaycastHit hitt;

            //if (Physics.Raycast(transform.position, dir, out hitt, ViewRadius, ObstacleMask))
            //{
            //    return new ViewCastInfo(true, hitt.point, hitt.distance, globalAngle);
            //}
            //else
            //{
            //    return new ViewCastInfo(false, transform.position + dir * ViewRadius, ViewRadius, globalAngle);


            if (hit)
            {
                Debug.DrawRay(transform.position, new Vector3(dir.x, dir.y, 0), Color.red);
                return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
            }
            else
            {
                return new ViewCastInfo(false, transform.position + (Vector3)dir * ViewRadius, ViewRadius, globalAngle);
                //Debug.DrawRay(transform.position, new Vector3(dir.x, dir.y, 0), Color.red);
            }
        }

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

        public Vector2 DirectionFromAngle(float angleInDegrees)
        {
            angleInDegrees += transform.eulerAngles.z;

            return new Vector2(-Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public struct ViewCastInfo
        {
            public bool hit;
            public Vector2 point;
            public float distance;
            public float angle;

            public ViewCastInfo(bool _hit, Vector2 _point, float _distance, float _angle)
            {
                hit = _hit;
                point = _point;
                distance = _distance;
                angle = _angle;
            }
        }

        #endregion
    }
}