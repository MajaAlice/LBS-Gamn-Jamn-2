using Unity.VisualScripting;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    // Main Script Refrences -Lud
    GameObject Player;
    CircleCollider2D PlayerCollider;
    Rigidbody2D PlayerBody;
    Player PlayerMain;

    // Main Script Data -Lud
    public float outerCircle;// How Big The Outer Circle Is
    public float innerCircle;// How Big The Inner Circle Is
    public float bouncy;     // So That You Lose An Amount Of Energy
    public Vector2 rotation; // For Rotation Check

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
        if (localPlayerPos.magnitude < outerCircle + PlayerCollider.radius)
        {
            Vector2 localDot = localPlayerPos;
            localDot.Normalize();

            // Trust Me Its Not -Lud :3
            bool notAShityWayToDoThis = false;
            if ((rotation.x > 0) && (rotation.y > 0)) { notAShityWayToDoThis = (localDot.x > 0) && (localDot.y > 0); }
            else if ((rotation.x > 0) && (rotation.y < 0)) { notAShityWayToDoThis = (localDot.x > 0) && (localDot.y < 0); }
            else if ((rotation.x < 0) && (rotation.y > 0)) { notAShityWayToDoThis = (localDot.x < 0) && (localDot.y > 0); }
            else if ((rotation.x < 0) && (rotation.y < 0)) { notAShityWayToDoThis = (localDot.x < 0) && (localDot.y < 0); }

            if (notAShityWayToDoThis)
            {
                // Checks If The Player Is Outside The Outer Circle
                if (localPlayerPos.magnitude > (outerCircle - PlayerCollider.radius))
                {
                    localPlayerPos.Normalize();
                    localPlayerPos *= outerCircle - PlayerCollider.radius;
                    PlayerBody.linearVelocity += localDot * -1;
                    PlayerBody.linearVelocity *= bouncy;
                }
                // Checks If The Player Is In The InnerCircle -Lud
                else if (localPlayerPos.magnitude < (innerCircle + PlayerCollider.radius))
                {
                    localPlayerPos.Normalize();
                    localPlayerPos *= innerCircle + PlayerCollider.radius;
                    PlayerBody.linearVelocity += localDot;
                    PlayerBody.linearVelocity *= bouncy;
                }

                // Compares To See What Direction Is Smaller -Lud
                if ((outerCircle - localPlayerPos.magnitude) < (localPlayerPos.magnitude - innerCircle))
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
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, outerCircle);
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(gameObject.transform.position, innerCircle);
    }
}
