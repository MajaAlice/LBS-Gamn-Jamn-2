using UnityEngine;
using UnityEngine.UIElements;

public class LineCollider : MonoBehaviour
{


    // Makes Sure The Normals Are Up To Date -Lud
    public void UpdateVector( Vector2 pointA, Vector2 pointB)
    {
        // Updates The Values -Lud
        Vector2 localPos = pointB - pointA;
        Vector2 normal = localPos;
        // Makes Sure The Sprite Is In The Correct Direction -Lud
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, normal.magnitude, gameObject.transform.localScale.z);
        normal.Normalize();
        gameObject.transform.rotation = Quaternion.LookRotation(normal) * Quaternion.Euler(0, -90, 0);
        gameObject.transform.position = new Vector2((pointA.x + pointB.x)/2, (pointA.y + pointB.y)/2);
    }

    // Makes Sure The Player Isnt Intersecting The Line -Lud
    /* not used anymore .-.
    public float lenght;
    public Vector2 normal;
    public Vector2 pointA;
    public Vector2 pointB;
    public Vector2 rightNormal;
    public Vector2 leftNormal;
    static float rigidity;
    static float tangentRigidity;
    static float lineTHICK;
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
    */
}
