using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{

    public float speed = 5f;

    private Rigidbody2D body;
    private Animator animator;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        playerWalk();
    }

    void playerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if(h == HorizontalConstants.right)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        else if(h == HorizontalConstants.left)
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
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

            Debug.Log("Body vel: " + body.velocity.x);
            Debug.Log("Stop decay: " + stop_decay);
            body.velocity = new Vector2(stop_decay, body.velocity.y);
        }
    }

}
