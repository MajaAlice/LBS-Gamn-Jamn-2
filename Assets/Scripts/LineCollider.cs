using UnityEngine;
using UnityEngine.UIElements;

public class LineCollider : MonoBehaviour
{
    // Main Script Refrences -Lud
    GameObject PlayerObject;
    CircleCollider2D PlayerCollider;
    Rigidbody2D PlayerBody;
    Player PlayerScript;

    public float lenght;
    public float rigidity;
    public float tangentRigidity;
    public Vector2 pointA;
    public Vector2 pointB;
    public Vector2 normal;
    public Vector2 rightNormal;
    public Vector2 leftNormal;


    void Start()
    {
        UpdateVector();

        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        PlayerCollider = PlayerObject.GetComponent<CircleCollider2D>();
        PlayerBody = PlayerObject.GetComponent<Rigidbody2D>();
        PlayerScript = PlayerObject.GetComponent<Player>();
    }

    // Makes Sure The Normals Are Up To Date -Lud
    public void UpdateVector()
    {
        // Updates The Values -Lud
        Vector2 localPos = pointB - pointA;
        lenght = localPos.magnitude;
        Vector2 Normal = localPos;
        Normal.Normalize();
        rightNormal = new Vector2(Normal.y, -Normal.x);
        leftNormal = new Vector2(-Normal.y, Normal.x);
        // Makes Sure The Sprite Is In The Correct Direction -Lud
        gameObject.transform.rotation = Quaternion.LookRotation(Normal);
    }

    void FixedUpdate()
    {
        Vector2 PlayerPos = PlayerObject.transform.position;
        if (lenght < (PlayerPos - pointA).magnitude)
        {
            CheckLineCollison();
        }
        else if (lenght < (PlayerPos - pointB).magnitude)
        {
            CheckLineCollison();
        }
    }

    // Makes Sure The Player Isnt Intersecting The Line -Lud
    public void CheckLineCollison()
    {
        Vector2 PlayerPos = PlayerObject.transform.position;
        float rightDistance = Vector2.Dot(PlayerPos, rightNormal);
        float leftDistance = Vector2.Dot(PlayerPos, leftNormal);
        if (PlayerCollider.radius > rightDistance)
        {
            // Moves The Player Off The Line -Lud
            PlayerObject.transform.position = PlayerPos + (rightNormal * (PlayerCollider.radius - rightDistance));
            float speedAlongNormal = Vector2.Dot(PlayerBody.linearVelocity, rightNormal);
            float speedAlongTangent = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(rightNormal.y, -rightNormal.x));

            if(speedAlongNormal <= 0)
            {
                // Credit Zanzlanz -Lud
                PlayerBody.linearVelocityX = -(speedAlongNormal * rightNormal.x) * rigidity + (speedAlongTangent * rightNormal.y) * tangentRigidity;
                PlayerBody.linearVelocityX = -(speedAlongNormal * rightNormal.x) * rigidity + (speedAlongTangent *-rightNormal.y) * tangentRigidity;
            }
        }
        else if (PlayerCollider.radius > leftDistance)
        {
            // Moves The Player Off The Line -Lud
            PlayerObject.transform.position = PlayerPos + (leftNormal * (PlayerCollider.radius - leftDistance));
            float speedAlongNormal = Vector2.Dot(PlayerBody.linearVelocity, leftNormal);
            float speedAlongTangent = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(leftNormal.y, -leftNormal.x));

            if (speedAlongNormal <= 0)
            {
                // Credit Zanzlanz -Lud
                PlayerBody.linearVelocityX = -(speedAlongNormal * leftNormal.x) * rigidity + (speedAlongTangent * leftNormal.y) * tangentRigidity;
                PlayerBody.linearVelocityX = -(speedAlongNormal * leftNormal.x) * rigidity + (speedAlongTangent * -leftNormal.y) * tangentRigidity;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(pointA.x, pointA.y), new Vector3(pointB.x, pointB.y));
    }
}
