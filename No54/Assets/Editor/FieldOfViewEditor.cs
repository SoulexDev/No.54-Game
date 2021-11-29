using UnityEditor;
using UnityEngine;

public class FieldOfViewEditor : MonoBehaviour
{
    public Animatronic animatronic;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Handles.DrawWireArc(animatronic.transform.position, Vector3.up, Vector3.forward, 360, 10);

        Vector3 viewAngle01 = DirectionFromAngle(animatronic.transform.eulerAngles.y, -animatronic.viewAngle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(animatronic.transform.eulerAngles.y, animatronic.viewAngle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle01 * 10);
        Gizmos.DrawLine(animatronic.transform.position, animatronic.transform.position + viewAngle02 * 10);

        if (animatronic.canSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(animatronic.transform.position, animatronic.player.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}