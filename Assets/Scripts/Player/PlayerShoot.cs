using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{

    public GameObject fire_bullet;

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }
    void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            GameObject bullet = Instantiate(fire_bullet, transform.position, Quaternion.identity);
            if(transform.localScale.x < 0)
            {
                bullet.GetComponent<FireBullet>().Speed *= -1;
            }
        }
    }
}
