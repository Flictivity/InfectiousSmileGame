using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed = 0.5f;
    private Vector2 moveVector;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.x == 0 && Input.GetAxis("Horizontal") == 0.52)
        {
            Destroy(gameObject);
            return;
        }
        body.velocity = new Vector3(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        if (Input.GetKeyDown(KeyCode.W))
        {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }
}
