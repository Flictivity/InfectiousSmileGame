using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed = 7f;
    public List<KeyCode> buttons = new List<KeyCode>();
    private bool _isOnGround = false;
    public bool IsTagged = false;
    public Transform Smile;

    public float GroundRadius = 0.2f;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    // Update is called once per frame
    void Update()
    {
        _isOnGround = Physics2D.OverlapCircle(GroundCheck.position, GroundRadius, WhatIsGround);
        if (Input.GetKey(buttons[1]))
        {
            body.velocity = new Vector3(-0.52f * speed, body.velocity.y);
        }
        else if (Input.GetKey(buttons[2]))
        {
            body.velocity = new Vector3(0.52f * speed, body.velocity.y);
        }

        if (Input.GetKeyDown(buttons[0]) && _isOnGround)
        {
            body.velocity = new Vector2(body.velocity.x, speed * 1.7f);
            _isOnGround = false;
        }
    }
}
