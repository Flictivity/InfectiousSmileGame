using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    private Rigidbody2D body;
    [HideInInspector] public float speed = 12f;
    [HideInInspector] public float jump = 12 * 1.7f;
    public List<KeyCode> buttons = new List<KeyCode>();
    private bool _isOnGround = false;
    public bool Shield = false;
    public Transform RespawnPoint;

    public float GroundRadius = 0.2f;
    public Transform GroundCheck;
    public LayerMask WhatIsGround;
    public Bonus Bonus;
    public bool IsBonusApply = false;
    public bool CanGetBonus = true;
    public List<GameObject> Players;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(6, 6);
    }

    void Update()
    {
        if (Bonus != null)
        {
            ApplyBonus();
        }
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
            body.velocity = new Vector2(body.velocity.x, jump);
            _isOnGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Water")
        {
            transform.position = RespawnPoint.position;
        }
    }

    private void ApplyBonus()
    {
        if (Bonus.Duration < 0)
        {
            Bonus = null;
            IsBonusApply = false;
            SetDefaultValues();
            return;
        }
        Bonus.Duration -= Time.deltaTime;
        if (!IsBonusApply)
        {
            switch (Bonus.Type)
            {
                case BonusType.SpeedBonus:
                    speed += 10f;
                    break;
                case BonusType.ShieldBonus:
                    var defaultColor = gameObject.GetComponent<SpriteRenderer>().material.color;
                    Shield = true;
                    gameObject.GetComponent<PlayerInfoScript>().Shield.gameObject.SetActive(true);
                    break;
                case BonusType.SlowTaggedBonus:
                    //if (Players == null)
                    //{
                    //    return;
                    //}
                    //var taggedPlayer = Players.FirstOrDefault(x => x.GetComponent<PlayerInfoScript>().IsTagged);
                    //taggedPlayer.GetComponent<PlayerMoveScript>().speed -= 10f;
                    break;
            }
            IsBonusApply = true;
        }


    }
    private void SetDefaultValues()
    {
        speed = 12f;
        Shield = false;
        gameObject.GetComponent<PlayerInfoScript>().Shield.gameObject.SetActive(false);
        //var taggedPlayer = Players.FirstOrDefault(x => x.GetComponent<PlayerInfoScript>().IsTagged);
        //taggedPlayer.GetComponent<PlayerMoveScript>().speed += 10f;
    }
}
