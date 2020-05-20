using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float move_speed = 1f;
    private Rigidbody2D body;
    private Animator animator;

    private bool move_left;

    public Transform down_collision;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        move_left = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move_left)
        {
            body.velocity = new Vector2(-move_speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(move_speed, body.velocity.y);
        }
        CheckCollision();
    }

    void CheckCollision()
    {
        if(!Physics2D.Raycast(down_collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        move_left = !move_left;

        Vector3 character_scale = transform.localScale;
        if (move_left)
        {
            character_scale.x = Mathf.Abs(character_scale.x);
        } 
        else
        {
            character_scale.x = -Mathf.Abs(character_scale.x);
        }

        transform.localScale = character_scale;
    }
}
