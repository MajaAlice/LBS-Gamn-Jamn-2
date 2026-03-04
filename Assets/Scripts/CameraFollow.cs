using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    //maja
    GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
    }
}
