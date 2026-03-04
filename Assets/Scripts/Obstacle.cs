using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class Obstacle : MonoBehaviour
{
    Animator Animator;
    private void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))
        {
            Death();
        }
    }
    public void Death()
    {
        Destroy(gameObject, 0.5f);
        Animator.Play("Explosion");        
    }
}
