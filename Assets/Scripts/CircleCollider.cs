using Unity.VisualScripting;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    GameObject Player;
    CircleCollider2D PlayerCollider;
    Rigidbody2D PlayerBody;
    Player PlayerMain;

    public float outerCircle;
    public float innerCircle;
    public float bouncy;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerCollider = Player.GetComponent<CircleCollider2D>();
        PlayerBody = Player.GetComponent<Rigidbody2D>();
        PlayerMain = Player.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        // Converts To Local Space AKA The 0,0 Is Now The Center Of Scripts Pearent -Lud
        Vector3 localPlayerPos = Player.transform.position - gameObject.transform.position;

        // Checks If It Should Check
        if (localPlayerPos.magnitude < outerCircle)
        {
            // Checks If The Player Is Outside The Outer Circle
            if (localPlayerPos.magnitude > (outerCircle - PlayerCollider.radius))
            {
                localPlayerPos.Normalize();
                localPlayerPos *= outerCircle - PlayerCollider.radius;
                Vector2 localDot = localPlayerPos * -1;
                localDot.Normalize();
                PlayerBody.linearVelocity += localDot;
                PlayerBody.linearVelocity *= bouncy;
            }
            // Checks If The Player Is In The InnerCircle -Lud
            else if (localPlayerPos.magnitude < (innerCircle + PlayerCollider.radius))
            {
                localPlayerPos.Normalize();
                localPlayerPos *= innerCircle + PlayerCollider.radius;
                Vector2 localDot = localPlayerPos;
                localDot.Normalize();
                PlayerBody.linearVelocity += localDot;
                PlayerBody.linearVelocity *= bouncy;
            }

            // Compares To See What Direction Is Smaller -Lud
            if ((outerCircle -localPlayerPos.magnitude) < (localPlayerPos.magnitude - innerCircle))
            {
                PlayerMain.SetThrust(outerCircle - localPlayerPos.magnitude);
            }
            else
            {
                PlayerMain.SetThrust(localPlayerPos.magnitude - innerCircle);
            }

            Player.transform.position = localPlayerPos + gameObject.transform.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, outerCircle);
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(gameObject.transform.position, innerCircle);
    }
}
