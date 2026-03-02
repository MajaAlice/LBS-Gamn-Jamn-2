using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{

    InputAction DirectionChange;
    InputAction Thrust;
    Rigidbody2D rb;

    public float rotationSpeed = 3;
    public float thrust = 2;
    static public float currentThrust;
    public float maxThrust = 10;

    void Start()
    {
        #region refrences
        //as of rn the input system is bugged and doesnt save settings, so iäm using default settigns -- maja
        DirectionChange = InputSystem.actions.FindAction("Move");
        Thrust = InputSystem.actions.FindAction("Jump");

        rb = gameObject.GetComponent<Rigidbody2D>();
        #endregion

        currentThrust = thrust;
    }
        
    private void FixedUpdate()
    {
        RotatePlayer();
        MoveForward();
    }


    //maja
    void RotatePlayer()
    {
        float direction = DirectionChange.ReadValue<Vector2>().x * -1;
        transform.rotation *= Quaternion.AngleAxis(direction * rotationSpeed * Time.fixedDeltaTime, Vector3.forward);
    }

    void MoveForward()
    {
        if (Thrust.ReadValue<float>() == 1)
        {
            rb.linearVelocity += new Vector2(transform.up.x, transform.up.y) * currentThrust * Time.fixedDeltaTime;
            Debug.Log(currentThrust);
        }
    }

    public void SetThrust(float distance)
    {
        currentThrust = thrust / distance;
        if (currentThrust > maxThrust)
        {
            currentThrust = 8;
        }

        if (currentThrust < thrust)
        {
            currentThrust = thrust;
        }
    }

}
