using UnityEngine;
using UnityEditor;

namespace UnknownGames
{
    [CustomEditor(typeof(PlayerFieldOfView))]
    [CanEditMultipleObjects]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            PlayerFieldOfView playerFOV = (PlayerFieldOfView)target;

            Handles.color = Color.white;
            Handles.DrawWireArc(playerFOV.transform.position, Vector3.forward, Vector3.right, 360, playerFOV.ViewRadius);

            Handles.color = Color.green;
            Vector3 viewAngleA = playerFOV.DirectionFromAngle(-playerFOV.ViewAngle / 2, false);
            Vector3 viewAngleB = playerFOV.DirectionFromAngle(playerFOV.ViewAngle / 2, false);

            Vector3 startPoint = playerFOV.transform.position;
            Vector3 endPointA = startPoint + viewAngleA * playerFOV.ViewRadius;
            Vector3 endPointB = startPoint + viewAngleB * playerFOV.ViewRadius;

            Handles.DrawLine(startPoint, endPointA);
            Handles.DrawLine(startPoint, endPointB);
        }
    }
}