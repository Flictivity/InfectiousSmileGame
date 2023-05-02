using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D body;
    private float speed = 7f;
    public int playerNumber = 0;
    public List<KeyCode> buttons = new List<KeyCode>();
    private bool _isOnGround = true;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
            body.velocity = new Vector2(body.velocity.x, speed);
            _isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            _isOnGround = true;
        }
    }
}
