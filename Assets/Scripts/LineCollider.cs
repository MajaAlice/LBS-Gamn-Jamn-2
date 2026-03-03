using UnityEngine;

public class LineCollider : MonoBehaviour
{
    GameObject PlayerObj;
    Player Player;

    public float lenght = 5;
    public Vector2 point1;
    public Vector2 point2;

    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        Player = PlayerObj.GetComponent<Player>();

        point1 = new Vector2(transform.up.x, transform.up.y);
        point2 = new Vector2(transform.up.x * lenght, transform.up.y * lenght);

    }

    void FixedUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3(point1.x, point1.y), new Vector3(point2.x, point2.y));
    }
}
