using UnityEngine;

public class ColliderShapeSpawner : MonoBehaviour
{
    public GameObject LineCollider;

    #region Circle variables
    public Vector3[] outerPoints;
    public Vector3[] innerPoints;

    public int segments = 5;
    public float angle = 90f;
    public float direction = 0f;
    public float innerRadius = 1f;
    public float outerRadius = 5f;
    #endregion

    private void Start()
    {
        outerPoints = new Vector3[segments + 1];
        innerPoints = new Vector3[segments + 1];
        Circle();
    }

    public void Circle()
    {
        transform.rotation = Quaternion.AngleAxis(direction, Vector3.forward);
        outerPoints[0] = transform.position + transform.up * outerRadius;
        innerPoints[0] = transform.position + transform.up * innerRadius;
        float stepSize = angle / segments;
        
        for(int i = 1; i < segments + 1; i++)
        {
            transform.rotation *= transform.rotation = Quaternion.AngleAxis(stepSize, Vector3.forward);
            outerPoints[i] = transform.position + transform.up * outerRadius;
            innerPoints[i] = transform.position + transform.up * innerRadius;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLineStrip(outerPoints, false);
        Gizmos.color = Color.purple;
        Gizmos.DrawLineStrip(innerPoints, false);
    }
}
