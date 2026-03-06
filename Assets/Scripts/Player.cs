using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class Player : MonoBehaviour
{

    InputAction DirectionChange;
    InputAction Thrust;
    InputAction Brake;

    GameObject BossObj;
    Boss Boss;

    Rigidbody2D rb;

    Animator Animator;

    GameObject MenuManager;
    MenuShit MenuShit;

    public float rotationSpeed = 3;
    public float thrust = 2;
    static public float currentThrust;
    public float maxThrust = 10;
    public float distanceMult = 0.5f;
    public float brakeStrenght = 0.95f; // a multiplier
    public bool hasControl = true;
    bool hasBossRef = false;
    public float behindPlayerCheckDistance = 4f;

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

        if (GameObject.FindGameObjectWithTag("Boss") != null)
        {
            BossObj = GameObject.FindGameObjectWithTag("Boss");
            Boss = BossObj.GetComponent<Boss>();
            hasBossRef = true;
        }


        Animator = gameObject.GetComponent<Animator>();

        MenuManager = GameObject.FindGameObjectWithTag("Menu");
        MenuShit = MenuManager.GetComponent<MenuShit>();
        #endregion

        currentThrust = thrust;
    }
        
    private void FixedUpdate()
    {
        if (hasControl)
        {
            RotatePlayer();
            MoveForward();
            SlowDown();
            BehindPlayerCheck();
        }
    }


    //maja´s code -Lud
    void RotatePlayer()
    {
        float direction = DirectionChange.ReadValue<Vector2>().x * -1;
        transform.rotation *= Quaternion.AngleAxis(direction * rotationSpeed * Time.fixedDeltaTime, Vector3.forward);
    }


    //gives momentum to the ship and sets animation -- Maja
    void MoveForward()
    {
        if (Thrust.ReadValue<float>() == 1)
        {
            rb.linearVelocity += new Vector2(transform.up.x, transform.up.y) * currentThrust * Time.fixedDeltaTime;
            Animator.Play("ShipThrust");
        }
        else { Animator.Play("ShipIdle"); }
        
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
        RaycastHit2D hitInfoObstacle = Physics2D.Raycast(transform.position, transform.up * -1, behindPlayerCheckDistance, Obstacle);
        Rigidbody2D affectedObject = hitInfoObstacle.rigidbody;
        RaycastHit2D hitInfoWall = Physics2D.Raycast(transform.position, transform.up * -1, behindPlayerCheckDistance, Wall);
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
 
    public IEnumerator Kill()
    {
        Animator.Play("Explosion");
        MenuShit.StopTimer();
        hasControl = false;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        if (hasBossRef)
        {
            Boss.isChasing = false;
        }
        gameObject.SetActive(false);
        new WaitForSeconds(1);
        MenuShit.ToggleDualUI(MenuShit.Menus.NULL, MenuShit.Menus.Death);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bag"))
        {
            StartCoroutine(Kill());
        }
    }
}
