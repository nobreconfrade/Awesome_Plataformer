using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator animator;
    private bool move_enable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        move_enable = true;
        StartCoroutine(DisableBullet());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (move_enable)
        {
            Vector3 bullet_transform = transform.position;
            bullet_transform.x += speed * Time.deltaTime;
            transform.position = bullet_transform;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer = 5f)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(
            collision.gameObject.tag == Tags.BEETLE 
            || collision.gameObject.tag == Tags.SNAIL 
            || collision.gameObject.tag == Tags.SPIDER
            || collision.gameObject.tag == Tags.BOSS
            )
        {
            animator.Play("bullet_explode");
            move_enable = false;
            StartCoroutine(DisableBullet(0.2f));
        }
    }
}
