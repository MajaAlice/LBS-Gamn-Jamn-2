using UnityEngine;
using UnityEngine.UIElements;

public class LineCollider : MonoBehaviour
{
    // Dumb Float Making Curves Look Solid While They Have Gaps -Lud
    public static float LineExtraThick = 0.15f;
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
    public static float rigidity;
    public static float tangentRigidity;

    private void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if(PlayerObject != null)
        {
            PlayerBody = PlayerObject.GetComponent<Rigidbody2D>();
            PlayerCollider = PlayerObject.GetComponent<CircleCollider2D>();
        }
    }

    // Makes Sure The Normals Are Up To Date -Lud [ The Names Should Be Fixed But I Am Just Making Sure The Scripts Dont Die]
    public void UpdateVector(Vector2 pointAUpdate, Vector2 pointBUpdate)
    {
        // Updates The Values -Lud
        Vector2 localPos = pointBUpdate - pointAUpdate;
        Vector2 normalUpdate = localPos;
        // Makes Sure The Sprite Is In The Correct Position -Lud
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, normalUpdate.magnitude + LineExtraThick, gameObject.transform.localScale.z);
        lenght = normalUpdate.magnitude;
        normalUpdate.Normalize();
        normal = normalUpdate;

        // Rotates The Game Object So It Doesnt Look Fucked -Lud
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(normalUpdate.y, normalUpdate.x) * Mathf.Rad2Deg + 90);
        gameObject.transform.position = new Vector2((pointAUpdate.x + pointBUpdate.x) / 2, (pointAUpdate.y + pointBUpdate.y) / 2);
    }
    public void CheckLineCollison()
    {
        Vector2 PlayerPos = PlayerObject.transform.position;
        Vector2 localPlayerPos = (Vector2)gameObject.transform.position - PlayerPos;
        float LengthDistance = Vector2.Dot(localPlayerPos, normal);
        if ((LengthDistance < lenght) && (LengthDistance > 0))
        {
            float distanceAlongNormal = Vector2.Dot(localPlayerPos, new Vector2(normal.y, -normal.x));
            if((distanceAlongNormal < PlayerCollider.radius) && ColliderSide)
            {
                // PlayerObject.transform.position += (Vector3)(new Vector2(normal.y, -normal.x) * (PlayerCollider.radius - distanceAlongNormal));

                float speedAlongNormal = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(normal.y, -normal.x));
                float speedAlongTangent = Vector2.Dot(PlayerBody.linearVelocity, new Vector2(-normal.x, -normal.y));

                if (speedAlongNormal <= 0)
                {
                    // Credit Zanzlanz -Lud
                    PlayerBody.linearVelocityX = -(speedAlongNormal * normal.y) * rigidity + (speedAlongTangent * -normal.x) * tangentRigidity;
                    PlayerBody.linearVelocityY = -(speedAlongNormal * -normal.x) * rigidity + (speedAlongTangent * -normal.y) * tangentRigidity;
                }
            }
            else if ((-distanceAlongNormal < PlayerCollider.radius) && !ColliderSide)
            {
                // Fuck ._. -Lud
            }
        }
    }
    
}
