using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 1;
    [SerializeField] Animator animator;
    Vector2 movVector;
    Vector3 Direction;

    void Update()
    {
        movVector = new Vector2(Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        AnimationManager();
    }

    private void FixedUpdate()
    {      
        Movement();
    }

    private void Movement()
    {
        rb.velocity = movVector;
    }
    public void AnimationManager()
    {
        animator.SetBool("isWalking", rb.velocity.magnitude > 0);
        Debug.Log("Faz o omega L");
        animator.SetFloat("HorizontalDirection", directionNormalized().x);
        animator.SetFloat("VerticalDirection", directionNormalized().y);
    }

    public Vector3 directionNormalized()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

            if (Input.GetAxisRaw("Vertical") == 0)
            {
                Direction.x = Input.GetAxisRaw("Horizontal");
                Direction.y = 0;
            }
            else if (Input.GetAxisRaw("Horizontal") == 0)
            {
                Direction.y = Input.GetAxisRaw("Vertical");
                Direction.x = 0;
            }
            else
            {
                Direction.x = Input.GetAxisRaw("Horizontal");
                Direction.y = Input.GetAxisRaw("Vertical");
            }
        }
        return Direction.normalized;
    }
}
