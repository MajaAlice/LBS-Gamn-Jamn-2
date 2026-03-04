using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ColliderShapeSpawner : MonoBehaviour
{
    //most of this script is by Maja, a little by oliver

    public GameObject LineColliderObj;

    public float rotation = 0f;
    #region Circle variables
    public Vector3[] ringOuterPoints;
    public Vector3[] ringInnerPoints;
    public int segments = 5;
    public float angle = 90f;
    public float ringInnerRadius = 1f;
    public float ringOuterRadius = 5f;
    #endregion

    #region line / tube variables
    public Vector3[] linePoints = new Vector3[2];
    public float lenght = 2f;
    public float distance = 2f;
    #endregion

    public shapeTypes selectedShape = shapeTypes.none;
    public enum shapeTypes
    {
        none,
        line,
        tube,
        curve,
        parallelCurve

    }
    private void Start()
    {
        ringOuterPoints = new Vector3[segments + 1];
        ringInnerPoints = new Vector3[segments + 1];

        switch (selectedShape)
        {
            case shapeTypes.line:
                Line();
                break;

            case shapeTypes.tube:
                Tube();
                break;

            case shapeTypes.curve: // both curve and parallel curve are in the same function, it's a bit spaghetti but it works -- Maja
                Curve();
                break;
            case shapeTypes.parallelCurve:
                Curve();
                break;
        }
    }

    public void Curve()
    {
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        ringOuterPoints[0] = transform.position + transform.up * ringOuterRadius;
        ringInnerPoints[0] = transform.position + transform.up * ringInnerRadius;
        float stepSize = angle / segments;
        
        for(int i = 1; i < segments + 1; i++)
        {
            transform.rotation *= transform.rotation = Quaternion.AngleAxis(stepSize, Vector3.forward);
            ringOuterPoints[i] = transform.position + transform.up * ringOuterRadius;
            if (selectedShape == shapeTypes.parallelCurve)
            {
                ringInnerPoints[i] = transform.position + transform.up * ringInnerRadius;
            }
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
            SpawnLine(ringOuterPoints[pointA], ringOuterPoints[pointB]);
            if(selectedShape == shapeTypes.parallelCurve)
            {
                SpawnLine(ringInnerPoints[pointA], ringInnerPoints[pointB]);
            }
        }
    }

    public void Line()
    {
        linePoints[0] = transform.position;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        linePoints[1] = transform.position + transform.up * distance;
        SpawnLine(linePoints[0], linePoints[1]);
    }

    public void Tube()
    {
        Line();
        transform.rotation = Quaternion.AngleAxis(90, Vector3.forward);
        transform.position += transform.up * distance;
        Line();
    }

    public void SpawnLine(Vector3 pointA, Vector3 pointB)
    {
        GameObject tempObj = Instantiate(LineColliderObj);
        LineCollider line = tempObj.GetComponent<LineCollider>();

        line.UpdateVector((Vector2)pointA, (Vector2)pointB);
    }
    
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLineStrip(outerPoints, false);
        //Gizmos.color = Color.purple;
        //Gizmos.DrawLineStrip(innerPoints, false);
    }
}
