using Unity.VisualScripting;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    GameObject Player;
    CircleCollider2D PlayerCollider;
    Rigidbody2D PlayerBody;
    public float outerCircle;
    public float innerCircle;
    public float bouncy;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerCollider = Player.GetComponent<CircleCollider2D>();
        PlayerBody = Player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Converts To Local Space AKA The 0,0 Is Now The Center Of Scripts Pearent -Lud
        Vector3 localPlayerPos = Player.transform.position - gameObject.transform.position;

        if (localPlayerPos.magnitude < outerCircle * 2)
        {
            if (localPlayerPos.magnitude > outerCircle - PlayerCollider.radius)
            {
                localPlayerPos = localPlayerPos.normalized * (outerCircle - PlayerCollider.radius);
                Vector2 localDot = localPlayerPos.normalized * -1;
                PlayerBody.linearVelocity *= localDot * bouncy;
            }
            else if (localPlayerPos.magnitude < innerCircle + PlayerCollider.radius)
            {
                localPlayerPos = localPlayerPos.normalized * (innerCircle + PlayerCollider.radius);
                Vector2 localDot = localPlayerPos.normalized;
                PlayerBody.linearVelocity *= localDot * bouncy;
            }

            Player.transform.position = localPlayerPos + gameObject.transform.position;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, outerCircle);
        Gizmos.color = Color.purple;
        Gizmos.DrawWireSphere(Vector3.zero, innerCircle);
    }
}
