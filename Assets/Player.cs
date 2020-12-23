using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Stats")]
    public float speed;
    public int score;
    public float jumpForce;
    private Rigidbody2D rb;
    public bool isGrounded;
    public float shotRate;
    public Transform shurikenSlot;
    private float timeSinceLastShot;
    private float horizontal;

    private int hp;
    public int currentHp;

    public bool shotDirRight;
    public SpriteRenderer sprite;[HideInInspector]
    public GameObject shuriken;[HideInInspector]

    private void Start()
    {
        hp = 10;
        currentHp = hp;
        
        
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }



    private void Update()
    {
        Movement();
        Jump();
        ShurikenThrow();
    }
    private void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;
        transform.position = transform.position + movement;
        if (horizontal > 0)
            sprite.flipX = false;
        else if (horizontal < 0)
            sprite.flipX = true;

    }
    private void Jump()
    {
        //Vector2 jump = new Vector2(0, 1)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Floor")
        {
            isGrounded = true;
        }

    }

    void ShurikenThrow()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time >= timeSinceLastShot)
        {
            Fire();
            timeSinceLastShot = Time.time + shotRate;
        }
        void Fire()
        {
            if (sprite.flipX == false)
            {
                shotDirRight = true;
                GameObject clone = Instantiate(shuriken, transform.position, transform.rotation);
            }
            else
            {
                shotDirRight = false;
                GameObject clone = Instantiate(shuriken, transform.position, transform.rotation);
            }
        }
    }
}