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
    }

}
