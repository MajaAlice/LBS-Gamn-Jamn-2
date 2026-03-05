using UnityEngine;
using UnityEngine.InputSystem;

public class SlowdownVFXDirection : MonoBehaviour
{
    GameObject PlayerObj;
    Rigidbody2D PlayerRB;
    InputAction Slowdown;
    SpriteRenderer SpriteRenderer;

    float rotation;

    void Start()
    {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        PlayerRB = PlayerObj.GetComponent<Rigidbody2D>();
        Slowdown = InputSystem.actions.FindAction("Sprint");
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 guh = PlayerRB.linearVelocity.normalized;
        rotation = Mathf.Atan2(guh.y, guh.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        if (Slowdown.ReadValue<float>() == 1)
        {
            Color funkyRed = new Vector4(255, 0, 0, 1);
            SpriteRenderer.color = funkyRed;
        }
        else { SpriteRenderer.color = Color.white; }
    }
}
