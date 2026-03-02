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
                localPlayerPos = localPlayerPos.normalized * outerCircle;
                Vector2 localDotA = localPlayerPos.normalized * -1;
                PlayerBody.linearVelocity *= bouncy;
                PlayerBody.linearVelocityX *= localDotA.x;
                PlayerBody.linearVelocityY *= localDotA.y;
            }
            else if (localPlayerPos.magnitude < innerCircle + PlayerCollider.radius)
            {
                localPlayerPos = localPlayerPos.normalized * innerCircle;
                Vector2 localDotA = localPlayerPos.normalized;
                PlayerBody.linearVelocity *= bouncy;
                PlayerBody.linearVelocityX *= localDotA.x;
                PlayerBody.linearVelocityY *= localDotA.y;
            }

            Player.transform.position = localPlayerPos + gameObject.transform.position;
        }
    }
}
