using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disable", 4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.PLAYER)
        {
            collision.GetComponent<PlayerDamage>().ReceiveDamage();
            gameObject.SetActive(false);

        }
    }
}
