using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public float superior_limit = 3f;
    public float inferior_limit = 3f;

    private Vector3 initial_position;

    private Animator animator;
    private Rigidbody2D rigid_body;
    private Vector3 move_direction = Vector3.down;
    private BoxCollider2D box_collider;

    private bool move_enable;

    public LayerMask player_layer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid_body = GetComponent<Rigidbody2D>();
        box_collider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        move_enable = true;
        initial_position = transform.position;
    }

    private void Update()
    {
        if(Physics2D.OverlapBox(transform.position, box_collider.size / 2, 0f, player_layer))
        {
            GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerDamage>().ReceiveDamage();
        }
        if (move_enable)
        {
            MoveSpider();
        }
    }

    void MoveSpider()
    {
        transform.Translate(move_direction * Time.smoothDeltaTime);
        //Debug.Log(transform.position);
        if (transform.position.y > initial_position.y + superior_limit)
        {
            move_direction = Vector3.down;
        }
        else if(transform.position.y < initial_position.y - inferior_limit)
        {
            move_direction = Vector3.up;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tags.BULLET)
        {
            animator.Play("spider_dead");
            move_enable = false;
            rigid_body.bodyType = RigidbodyType2D.Dynamic;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(Dead());
        }
        
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}
