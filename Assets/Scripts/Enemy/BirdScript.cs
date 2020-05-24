using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    private Rigidbody2D rigid_body;
    private Animator animator;

    private Vector3 move_direction = Vector3.left;
    private Vector3 origin_position;
    private Vector3 move_position;

    public GameObject egg;
    public LayerMask player_layer;

    private bool attacked;
    private bool move_enable;

    private void Awake()
    {
        rigid_body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        origin_position = transform.position;
        origin_position.x += 6f;
        move_position = transform.position;
        move_position.x -= 6f;
        move_enable = true;
    }

    void Update()
    {
        BirdMovement();
    }

    void BirdMovement()
    {
        if (move_enable)
        {
            transform.Translate(move_direction * Time.smoothDeltaTime);
            if(transform.position.x >= origin_position.x)
            {
                move_direction = Vector3.left;
                ChangeDirection();
            }
            else if (transform.position.x <= move_position.x)
            {
                move_direction = Vector3.right;
                ChangeDirection();
            }
        }
    }

    void ChangeDirection()
    {
        Vector3 character_scale = transform.localScale;
        character_scale.x *= -1;
        transform.localScale = character_scale;
    }

}
