using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public float speed; 
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }


    void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        // Looks at the player and follows the player to their position - Oliver
        Vector2 OffsetVector = transform.position - Player.transform.position;
        float Angle = Mathf.Atan2(OffsetVector.y, OffsetVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, Angle + 90);

        transform.position += transform.up * (speed * Time.fixedDeltaTime);
    }
    


}

