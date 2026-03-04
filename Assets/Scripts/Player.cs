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

    LayerMask Obstacle;
    LayerMask Wall;

    void Start()
    {
        #region refrences
        //as of rn the input system is bugged and doesnt save settings, so iäm using default settigns -- maja
        DirectionChange = InputSystem.actions.FindAction("Move");
        Thrust = InputSystem.actions.FindAction("Jump");
        Brake = InputSystem.actions.FindAction("Sprint");

        rb = gameObject.GetComponent<Rigidbody2D>();
        Obstacle = LayerMask.GetMask("Obstacle");
        Wall = LayerMask.GetMask("Wall");
        #endregion

        currentThrust = thrust;
    }
        
    private void FixedUpdate()
    {
        RotatePlayer();
        MoveForward();
        SlowDown();
        BehindPlayerCheck();
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
            // Hopefully This Will Give Brake More Controll -Lud
            Vector2 Vel = rb.linearVelocity;
            if (Vel.magnitude > 0.5f)
            {
                Vel.Normalize();
                rb.linearVelocity -= Vel * (currentThrust * Time.fixedDeltaTime * brakeStrenght);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    }


    public void SetThrust(float distance)
    {
        distance = distance * distanceMult;
        currentThrust = thrust / (distance * distance);
        if (currentThrust > maxThrust)
        {
            currentThrust = maxThrust;
        }

        if (currentThrust < thrust)
        {
            currentThrust = thrust;
        }
    }


    //crude af code that checks for objects behind the player and pushes objects, also triggers SetThrust() -- Maja
    public void BehindPlayerCheck()
    {
        RaycastHit2D hitInfoObstacle = Physics2D.Raycast(transform.position, transform.up * -1, 2, Obstacle);
        Rigidbody2D affectedObject = hitInfoObstacle.rigidbody;
        RaycastHit2D hitInfoWall = Physics2D.Raycast(transform.position, transform.up * -1, 2, Wall);
        if(hitInfoObstacle)
        {
            SetThrust(hitInfoObstacle.distance);
            if(Thrust.ReadValue<float>() == 1)
            {
                affectedObject.AddForce(transform.up * currentThrust * -1);
            }
        }
        else { currentThrust = thrust; }
        if (hitInfoWall) 
        {
            SetThrust(hitInfoWall.distance);
        }
        else { currentThrust = thrust; }
    }

}
