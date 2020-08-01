using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthScript : MonoBehaviour
{

    public int health = 10;

    private Animator animator;

    private bool can_damage;

    private void Awake()
    {
        animator = GetComponent <Animator>();
        can_damage = true;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        can_damage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (can_damage)
        {
            if(collision.tag == Tags.BULLET)
            {
                health--;
                can_damage = false;
                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBossScript();
                    animator.Play("boss_death");
                }
                StartCoroutine(WaitForDamage());
            }
        }
    }
}
