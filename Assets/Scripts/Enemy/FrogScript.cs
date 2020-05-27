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
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        jump_left = true;
        StartCoroutine(frog_jump_corountine);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(frog_jump_timer);
        if (jump_left)
        {
            animator.Play("frog_jump_left");
        }

        StartCoroutine(frog_jump_corountine);
    }

    void AfterAnimationFinished()
    {
        animator.Play("frog_idle_left");
    }
}
