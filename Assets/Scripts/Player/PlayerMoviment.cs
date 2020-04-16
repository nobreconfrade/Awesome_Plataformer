using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D body;
    private Animator animator;
    private Transform transform;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;

    public float jumpPower = 5f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        checkGrounded();
        playerJump();
    }

    void FixedUpdate()
    {
        playerWalk();
    }

    void playerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        Vector3 character_scale = transform.localScale;
        if(h == HorizontalConstants.right)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
            character_scale.x = 1;
        }
        else if(h == HorizontalConstants.left)
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
            character_scale.x = -1;

        }
        else
        {
            float stop_decay = 0f;
            if (body.velocity.x > 0)
            {
                stop_decay = body.velocity.x - speed * 0.10f;
                if (stop_decay < 0)
                    stop_decay = 0f;
            }
            else if (body.velocity.x < 0)
            {
                stop_decay = body.velocity.x + speed * 0.10f;
                if (stop_decay > 0f)
                    stop_decay = 0f;
            }

            body.velocity = new Vector2(stop_decay, body.velocity.y);
        }

        transform.localScale = character_scale; // https://www.youtube.com/watch?v=k-75tAys7iI
        animator.SetInteger("animator_speed", Mathf.Abs((int)body.velocity.x));

    }

    void checkGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                animator.SetBool("animator_jump", false);
            }
        }
    }

    void playerJump()
    {
        if (isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumped = true;
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                animator.SetBool("animator_jump", true);
            }
        }
    }
}
