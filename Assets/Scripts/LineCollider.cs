using UnityEngine;

public class LineCollider : MonoBehaviour
{
    // Main Script Refrences -Lud
    GameObject PlayerObject;
    CircleCollider2D PlayerCollider;
    Rigidbody2D PlayerBody;
    Player PlayerScript;

    public float lenght = 5;
    public Vector2 pointA;
    public Vector2 pointB;
    public Vector2 rightNormal;
    public Vector2 leftNormal;


    void Start()
    {
        UpdateVector();

        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        /*
        PlayerCollider = PlayerObject.GetComponent<CircleCollider2D>();
        PlayerBody = PlayerObject.GetComponent<Rigidbody2D>();
        PlayerScript = PlayerObject.GetComponent<Player>();
        */
    }

    // Makes Sure The Normals Are Up To Date -Lud
    public void UpdateVector()
    {
        Vector2 localPos = pointB - pointA;
        Vector2 Normal = localPos;
        Normal.Normalize();
        rightNormal = new Vector2(Normal.y, -Normal.x);
        leftNormal = new Vector2(-Normal.y, Normal.x);
    }

    void FixedUpdate()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(pointA.x, pointA.y), new Vector3(pointB.x, pointB.y));
    }
}
