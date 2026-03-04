using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public float speedUpMult;
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate()
    {
        TrackPlayer();
        MoveTowardsPlayer();
    }

    void TrackPlayer()
    {
        // Looks at the player and follows the player to their position - Oliver
        Vector2 OffsetVector = transform.position - Player.transform.position;
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);
    }
    
    public void MoveTowardsPlayer()
    {
        float currentSpeed;
        float distance = (Player.transform.position - transform.position).magnitude;
        currentSpeed = distance * speedUpMult;
        if(currentSpeed < speed) { currentSpeed = speed; }
        transform.position += transform.up * (currentSpeed * Time.fixedDeltaTime);
    }
}

