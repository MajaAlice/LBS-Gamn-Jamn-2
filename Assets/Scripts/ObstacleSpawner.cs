using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject Obstacle;
    public int amount;
    public Vector2 point1;
    public Vector2 point2;

    void Start()
    {
        for(int i = amount; i > 0; i--)
        {
            Instantiate(Obstacle, new Vector3(Random.Range(point1.x, point2.x), Random.Range(point1.y, point2.y), 0), Quaternion.Euler(0,0,0));
        }
    }
}
