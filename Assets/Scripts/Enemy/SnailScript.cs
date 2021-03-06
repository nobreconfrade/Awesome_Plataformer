﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float move_speed = 1f;
    public float ragdoll_coefficient = 5f;

    private Rigidbody2D rigid_body;
    private Animator animator;

    public LayerMask player_layer;


    private bool move_left;
    private bool move_enable;
    private bool stunned;

    public Transform down_collision, left_collision, right_collision, top_collision;
    private Vector3 left_collision_position, right_collision_position;

    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        left_collision_position = left_collision.position;
        right_collision_position = right_collision.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        move_left = true;
        move_enable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move_enable)
        {
            if (move_left)
            {
                rigid_body.velocity = new Vector2(-move_speed, rigid_body.velocity.y);
            }
            else
            {
                rigid_body.velocity = new Vector2(move_speed, rigid_body.velocity.y);
            }

        }
        CheckCollision();
    }

    void GetHit()
    {
        move_enable = false;
        rigid_body.velocity = new Vector2(0, 0);
        if (CompareTag(Tags.SNAIL))
        {
            //rigid_body.constraints = RigidbodyConstraints2D.None; // this will unlock rotation, very cool on stunned snail
            animator.Play("snail_stunned");
        }
        else if (CompareTag(Tags.BEETLE))
        {
            animator.Play("beetle_stunned");
            StartCoroutine(Dead(0.8f));
        }
        stunned = true;
    }
    void CheckCollision()
    {

        RaycastHit2D left_hit = Physics2D.Raycast(left_collision.position, Vector2.left, 0.1f, player_layer);
        RaycastHit2D right_hit = Physics2D.Raycast(right_collision.position, Vector2.right, 0.1f, player_layer);

        Collider2D top_hit = Physics2D.OverlapCircle(top_collision.position, 0.2f, player_layer);

        if(top_hit != null)
        {
            if(top_hit.gameObject.tag == Tags.PLAYER)
            {
                if (!stunned)
                {
                    top_hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(top_hit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    GetHit();
                }
            }
        }

        if (left_hit)
        {
            if(left_hit.collider.gameObject.tag == Tags.PLAYER)
            {
                if (!stunned)
                {
                    left_hit.collider.gameObject.GetComponent<PlayerDamage>().ReceiveDamage();
                }
                else
                {
                    if (CompareTag(Tags.SNAIL))
                    {
                        rigid_body.velocity = new Vector2(ragdoll_coefficient, rigid_body.velocity.y);
                    }
                }
            }
        }

        if (right_hit)
        {
            if(right_hit.collider.gameObject.tag == Tags.PLAYER)
            {
                if (!stunned)
                {
                    right_hit.collider.gameObject.GetComponent<PlayerDamage>().ReceiveDamage();
                }
                else
                {
                    if (CompareTag(Tags.SNAIL))
                    {
                        rigid_body.velocity = new Vector2(-ragdoll_coefficient, rigid_body.velocity.y);
                    }
                }
            }
        }

        if (!Physics2D.Raycast(down_collision.position, Vector2.down, 0.1f))
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
            left_collision.position = left_collision_position;
            right_collision.position = right_collision_position;
        } 
        else
        {
            character_scale.x = -Mathf.Abs(character_scale.x);
            left_collision.position = right_collision_position;
            right_collision.position = left_collision_position;
        }

        transform.localScale = character_scale;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == Tags.BULLET)
        {
            if (!stunned)
            {
                GetHit();
            }
        }
    }

}
