using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BonusBlock : MonoBehaviour
{
    public Transform bottom_collision;
    public LayerMask playerLayer;
    public GameObject coin;

    private GameObject coin_clone;
    private Text coin_text_score;
    private Animator animator;

    private Vector3 move_direction = Vector3.up;
    private Vector3 origin_position;
    private Vector3 animator_position;
    private bool start_animation;
    private bool start_coin_animation;
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
        coin_text_score = GameObject.Find("Coin text").GetComponent<Text>();
    }

    private void Update()
    {
        CheckForCollision();
        AnimateUpDown();
        AnimateCoin();
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
                    coin_clone = Instantiate(
                    coin,
                    new Vector3(
                        transform.position.x,
                        transform.position.y + 0.1f,
                        transform.position.z
                        ),
                    Quaternion.identity
                    );
                    animator.Play("bonus_block_idle");
                    start_animation = true;
                    can_animate = false;
                    start_coin_animation = true;
                    AddToCoinScore();
                    StartCoroutine(DisableCoin());
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

    void AnimateCoin()
    {
        if(start_coin_animation)
        {
            coin_clone.transform.Translate(Vector3.up * Time.smoothDeltaTime * 5);
            if (coin_clone.transform.position.y >= origin_position.y + 0.8f)
            {
                start_coin_animation = false;
            }
        }
    }

    void AddToCoinScore()
    {
        string[] score_text;
        int score;
        score_text = coin_text_score.text.Split('x');
        score = int.Parse(score_text[1]);
        score++;
        coin_text_score.text = 'x' + score.ToString();
    }

    IEnumerator DisableCoin()
    {
        yield return new WaitForSeconds(0.8f);
        coin_clone.SetActive(false);
    }


}
