using NUnit.Framework;
using UnityEngine;

public class ColliderShapeSpawner : MonoBehaviour
{
    public GameObject LineColliderObj;

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
        for (int i = segments; i > 0; i--)
        {            
            int pointA = i;
            int pointB;
            if (i == 0)
            {
                pointB = 0;
            }
            else pointB = i - 1;
            SpawnLine(outerPoints[pointA], outerPoints[pointB]);
            SpawnLine(innerPoints[pointA], innerPoints[pointB]);
        }
    }


    public void SpawnLine(Vector3 pointA, Vector3 pointB)
    {
        GameObject tempObj = Instantiate(LineColliderObj);
        LineCollider line = tempObj.GetComponent<LineCollider>();
        line.pointA = new Vector2(pointA.x, pointA.y);
        line.pointB = new Vector2(pointB.x, pointB.y);
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLineStrip(outerPoints, false);
        //Gizmos.color = Color.purple;
        //Gizmos.DrawLineStrip(innerPoints, false);
    }
}
