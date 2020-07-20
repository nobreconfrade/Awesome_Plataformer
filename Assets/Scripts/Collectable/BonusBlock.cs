using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    public Transform bottom_collision;
    public LayerMask playerLayer;

    private Animator animator;

    private Vector3 move_direction = Vector3.up;
    private Vector3 origin_position;
    private Vector3 animator_position;
    private bool start_animation;
    private bool can_animate;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        origin_position = transform.position;
        animator_position = transform.position;
        animator_position.y += 0.15f;
        can_animate = true;
    }

    private void Update()
    {
        CheckForCollision();
        AnimateUpDown();
    }

    void CheckForCollision()
    {
        if (can_animate)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottom_collision.position, Vector2.down, 0.1f, playerLayer);
            if (hit)
            {
                if (hit.collider.gameObject.tag == Tags.PLAYER)
                {
                    // score code
                    animator.Play("bonus_block_idle");
                    start_animation = true;
                    can_animate = false;
                }
            }
        }
    }
    
    void AnimateUpDown()
    {
        if (start_animation)
        {
            transform.Translate(move_direction * Time.smoothDeltaTime);
            if(transform.position.y >= animator_position.y)
            {
                move_direction = Vector3.down;
            }
            else if (transform.position.y <= origin_position.y)
            {
                start_animation = false;
            }
        }
    }

}
