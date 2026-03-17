using UnityEngine;
using UnityEngine.UIElements;

public class LineCollider : MonoBehaviour
{
    // Player Veriables -Lud
    public GameObject PlayerObject;
    public Rigidbody2D PlayerBody;
    public CircleCollider2D PlayerCollider;
    // Line Veriables -Lud
    public float lenght;
    public bool ColliderSide = true;
    public Vector2 pointA;
    public Vector2 pointB;
    public Vector2 normal;
    public float rigidity;
    public float tangentRigidity;

    private void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObject != null)
        {
            PlayerBody = PlayerObject.GetComponent<Rigidbody2D>();
            PlayerCollider = PlayerObject.GetComponent<CircleCollider2D>();
        }
        UpdateVector();
    }

    void Update()
    {
        CheckLineCollison();
    }

    // Makes Sure The Normals Are Up To Date -Lud [ The Names Should Be Fixed But I Am Just Making Sure The Scripts Dont Die]
    public void UpdateVector()
    {
        // Updates The Values -LudpointBUpdate
        Vector2 localPos = pointB - pointA;
        Vector2 normalUpdate = localPos;
        // Makes Sure The Sprite Is In The Correct Position -Lud
        lenght = normalUpdate.magnitude;
        normalUpdate.Normalize();
        // Sets The Values...
        normal = normalUpdate;

        // Rotates The Game Object So It Doesnt Look Fucked -Lud
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(normal.y, normal.x) * Mathf.Rad2Deg + 90);
        transform.position = new Vector2((pointA.x + pointB.x) / 2, (pointA.y + pointB.y) / 2);
        transform.localScale = new Vector3(0.5f, lenght, 0.5f);
    }
    public void CheckLineCollison()
    {
        Vector2 PlayerPos = PlayerObject.transform.position;
        Vector2 localPlayerPos = PlayerPos - (Vector2)transform.position;
        float LengthDistance = Vector2.Dot(PlayerPos - pointA, normal);
        Debug.Log("LengthDistance : " + LengthDistance);
        if ((LengthDistance < lenght) && (LengthDistance > 0))
        {
            float distanceFromLine = Vector2.Dot(localPlayerPos, new Vector2(normal.y, -normal.x));
            Debug.Log("DistanceFromLine : " + distanceFromLine);
            if((distanceFromLine < PlayerCollider.radius) && (distanceFromLine > -1))
            {
                PlayerObject.transform.position += (Vector3)(new Vector2(normal.y, -normal.x) * (PlayerCollider.radius - distanceFromLine));

                float speedAlongNormal = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(normal.y, -normal.x));
                float speedAlongTangent = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(-normal.x, -normal.y));

                if (speedAlongNormal <= 0)
                {
                    // Credit Zanzlanz -Lud
                    PlayerBody.linearVelocityX = -(speedAlongNormal * normal.y) * rigidity + (speedAlongTangent * -normal.x) * tangentRigidity;
                    PlayerBody.linearVelocityY = -(speedAlongNormal * -normal.x) * rigidity + (speedAlongTangent * -normal.y) * tangentRigidity;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 Pos = gameObject.transform.position;
        Gizmos.DrawLine(Pos, new Vector2(normal.y, -normal.x) + Pos);
    }
}
