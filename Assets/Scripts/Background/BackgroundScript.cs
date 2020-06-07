using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer sprite_renderer = GetComponent<SpriteRenderer>();

        transform.localScale = new Vector3(1, 1, 1);

        float width = sprite_renderer.sprite.bounds.size.x;
        float height = sprite_renderer.sprite.bounds.size.y;
        
        float worldHeight = Camera.main.orthographicSize * 2;
        Debug.Log(worldHeight);
        float worldWidth = worldHeight / Screen.height * Screen.width;
        Debug.Log(worldWidth);


        Vector3 background_scale = transform.localScale;
        background_scale.x = worldWidth / width + 0.2f;
        background_scale.y = worldHeight / height + 0.2f;
        transform.localScale = background_scale;
    }

}
