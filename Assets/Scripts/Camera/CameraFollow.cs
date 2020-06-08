using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float reset_speed = 0.5f;
    public float camera_speed = 0.3f;
    public Bounds camera_bounds;
    private Transform target;
    private float offset_z;
    private Vector3 last_target_position;
    private Vector3 current_velocity;
    private bool follows_players;

    private void Awake()
    {
        BoxCollider2D box_collider = GetComponent<BoxCollider2D>();
        box_collider.size = new Vector2(Camera.main.aspect * 2 * Camera.main.orthographicSize, 15f);
        camera_bounds = box_collider.bounds;
    }

    private void Start()
    {
        target = GameObject.FindWithTag(Tags.PLAYER).transform;
        last_target_position = target.position;
        offset_z = (transform.position - target.position).z;
        follows_players = true;
    }

    private void FixedUpdate()
    {
        if (follows_players)
        {
            Vector3 ahead_target_pos = target.position + Vector3.forward * offset_z;
            if(ahead_target_pos.x > transform.position.x)
            {
                Vector3 new_camera_pos = Vector3.SmoothDamp(
                    transform.position, ahead_target_pos, ref current_velocity, camera_speed);
                transform.position = new Vector3(new_camera_pos.x, transform.position.y, new_camera_pos.z);

                last_target_position = target.position;
            }
        }
    }

}
