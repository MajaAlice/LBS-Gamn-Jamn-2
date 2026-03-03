using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{

    InputAction DirectionChange;
    InputAction Thrust;
    InputAction Brake;

    Rigidbody2D rb;

    public float rotationSpeed = 3;
    public float thrust = 2;
    static public float currentThrust;
    public float maxThrust = 10;
    public float distanceMult = 0.5f;
    public float brakeStrenght = 0.95f; // a multiplier

    void Start()
    {
        #region refrences
        //as of rn the input system is bugged and doesnt save settings, so iäm using default settigns -- maja
        DirectionChange = InputSystem.actions.FindAction("Move");
        Thrust = InputSystem.actions.FindAction("Jump");
        Brake = InputSystem.actions.FindAction("Sprint");

        rb = gameObject.GetComponent<Rigidbody2D>();
        #endregion

        currentThrust = thrust;
    }
        
    private void FixedUpdate()
    {
        RotatePlayer();
        MoveForward();
        SlowDown();
        Debug.Log(rb.linearVelocity);
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
        }
    }

    public void SlowDown()
    {
        if(Brake.ReadValue<float>() == 1)
        {
            rb.linearVelocity *= brakeStrenght;
        }
    }


    //public void SetThrust(float distance)
    //{
    //    distance = distance * distanceMult;
    //    currentThrust = thrust / (distance * distance);
    //    if (currentThrust > maxThrust)
    //    {
    //        currentThrust = 8;
    //    }

    //    if (currentThrust < thrust)
    //    {
    //        currentThrust = thrust;
    //    }
    //}

}
