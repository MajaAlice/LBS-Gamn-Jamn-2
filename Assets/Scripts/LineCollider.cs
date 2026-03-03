using UnityEngine;
using UnityEngine.UIElements;

public class LineCollider : MonoBehaviour
{
    // Main Script Refrences -Lud
    GameObject PlayerObject;
    CircleCollider2D PlayerCollider;
    Rigidbody2D PlayerBody;
    Player PlayerScript;

    static float lineTHICK;
    public float lenght;
    static float rigidity;
    static float tangentRigidity;
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
        normal = localPos;
        normal.Normalize();
        rightNormal = new Vector2(normal.y, -normal.x);
        leftNormal = new Vector2(-normal.y, normal.x);
        // Makes Sure The Sprite Is In The Correct Direction -Lud
        gameObject.transform.rotation = Quaternion.LookRotation(normal);
        gameObject.transform.position = new Vector3((pointA.x + pointB.x)/2, (pointA.y + pointB.y)/2, 0);
    }

    void Update()
    {
        CheckLineCollison();
    }

    // Makes Sure The Player Isnt Intersecting The Line -Lud
    public void CheckLineCollison()
    {
        Vector2 PlayerPos = PlayerObject.transform.position;
        Vector2 localPlayerPos = gameObject.transform.position - PlayerObject.transform.position;
        float LengthDistance = Vector2.Dot(localPlayerPos, normal);
        if ((LengthDistance < lenght) && (LengthDistance > 0))
        {
            float distanceAlongNormal = Vector2.Dot(localPlayerPos, rightNormal);
            if(Mathf.Abs(distanceAlongNormal) < PlayerCollider.radius + lineTHICK)
            {
                float speedAlongNormal = Vector2.Dot(PlayerBody.linearVelocity, rightNormal);
                float speedAlongNormalLeft = Vector2.Dot(PlayerBody.linearVelocity, leftNormal);
                float speedAlongTangent = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(rightNormal.y, -rightNormal.x));
                float speedAlongTangentLeft = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(leftNormal.y, -leftNormal.x));

                if (speedAlongNormal <= 0)
                {
                    // Credit Zanzlanz -Lud
                    PlayerBody.linearVelocityX = -(speedAlongNormal * rightNormal.x) * rigidity + (speedAlongTangent * rightNormal.y) * tangentRigidity;
                    PlayerBody.linearVelocityY = -(speedAlongNormal * rightNormal.y) * rigidity + (speedAlongTangent * -rightNormal.x) * tangentRigidity;
                }
                else if (speedAlongNormalLeft <= 0)
                {
                    // Credit Zanzlanz -Lud
                    PlayerBody.linearVelocityX = -(speedAlongNormalLeft * leftNormal.x) * rigidity + (speedAlongTangentLeft * leftNormal.y) * tangentRigidity;
                    PlayerBody.linearVelocityY = -(speedAlongNormalLeft * leftNormal.y) * rigidity + (speedAlongTangentLeft * -leftNormal.x) * tangentRigidity;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(pointA.x, pointA.y), new Vector3(pointB.x, pointB.y));
    }
}
