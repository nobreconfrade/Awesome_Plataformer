using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator animator;
    private bool animation_started;
    private bool animation_finished;

    private int jumps;
    private bool jump_left;

    public float frog_jump_timer = 2f;

    private string frog_jump_corountine = "FrogJump";

    public LayerMask player_layer;

    private GameObject player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        jump_left = true;
        jumps = 0;
        StartCoroutine(frog_jump_corountine);
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER);

    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, 0.5f, player_layer))
        {
            player.GetComponent<PlayerDamage>().ReceiveDamage();
        }
    }

    void LateUpdate()
    {
        if(animation_finished && animation_started)
        {
            animation_started = false;
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }    
    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(frog_jump_timer);

        animation_started = true;
        animation_finished = false;

        if (jump_left)
        {
            animator.Play("frog_jump_left");
        }
        else
        {
            animator.Play("frog_jump_right");
        }

        jumps++;

        StartCoroutine(frog_jump_corountine);
    }

    void AfterAnimationFinished()
    {
        animation_finished = true;

        if (jump_left)
        {
            animator.Play("frog_idle_left");
        }
        else
        {
            animator.Play("frog_idle_right");
        }

        if (jumps == 3)
        {
            jumps = 0;
            Vector3 character_scale = transform.localScale;
            character_scale.x *= -1;
            transform.localScale = character_scale;

            jump_left = !jump_left;
        }
    }
}
