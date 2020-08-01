using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public GameObject stone;
    public Transform attackInstantiate;
    
    private Animator animator;

    private string coroutine_name = "StartAttack";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(coroutine_name);
    }

    void Update()
    {
        
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700f), 0f));
    }

    void BackToIdle()
    {
        animator.Play("boss_idle");
    }

    public void DeactivateBossScript()
    {
        StopAllCoroutines();
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(3f);
        animator.Play("boss_attack");
        StartCoroutine(coroutine_name);
    }

}
