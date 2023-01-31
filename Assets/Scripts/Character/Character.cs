using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.Utilities;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Character : MonoBehaviour
{
    public static Character instance;
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Animator anim;

    [Title("PlayerStats", null, TitleAlignments.Centered)]
    public int machineParts;
    public float movementSpeed;
    public float maxHealth;
    public float curHealth;
    private float initSpeed;
    private float initShotRate;
    //When true desires go unfulfilled, they turn into needs.

    [Title("Character Setting and Dependencies", null, TitleAlignments.Centered)]
    public bool waitForSpawn;
    public ShootingManager shooting;
    private Shield shield;
    [SerializeField]
    private float rotationLerp;
    public enum PlayerState { Slowdown, Walking, Dash, Shooting };
    public PlayerState state;
    Vector2 mousePos;
    private LayerMask enemyBullet = 12;


    private void Awake()
    {
        shield = GetComponent<Shield>();
        shooting = GetComponent<ShootingManager>();
        initSpeed = movementSpeed;
        initShotRate = shooting.abilityShotRate;
        anim = GetComponent<Animator>();
        instance = this;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        maxHealth = 100;
        curHealth = 100;
    }

    private void Update()
     {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            switch (state)
            {
                case (PlayerState.Walking):
                    UIManager.instance.playerState.text = "PlayerState: Walking";
                    break;
                case (PlayerState.Dash):
                    UIManager.instance.playerState.text = "PlayerState: Dash";
                    break;
                case (PlayerState.Shooting):
                    UIManager.instance.playerState.text = "PlayerState : Shooting";
                    break;
                case (PlayerState.Slowdown):
                    UIManager.instance.playerState.text = "PlayerState : Slowdown";
                    break;
        }
    }
    private  void FixedUpdate()
    {
            Movement();
            PlayerRotation();
    }
    #region movement and rotation
    private void Movement()
    {
        state = PlayerState.Walking;
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * movementSpeed;
        moveVector = Vector3.ClampMagnitude(moveVector, 1);
        transform.position += moveVector;
    }

    private void PlayerRotation()
    {
        //rotation not based on rigidbody.

          Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          difference.Normalize();
          float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg -90;
          var targetTransform = Quaternion.Euler(0f, 0f, rotationZ) ;
          transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform, rotationLerp);

        //rotation based on rigidbody.

       /* Vector2 direction = mousePos - rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;/*

        //rotation with perspective camera(3D)

        /*  Ray mousray = Camera.main.ScreenPointToRay(Input.mousePosition);
          float midpoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
          transform.LookAt(mousray.origin + mousray.direction * midpoint);   */
    }
    #endregion

    //Manage store with events that subscribe from different classes.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Store" && waitForSpawn == true) //Store open
        {
            var store = collision.GetComponent<Store>();
            //open store
        }
        if (collision.gameObject.layer == enemyBullet && shield == null) //Bullet damage
        {
            Damage(collision.GetComponent<EnemyBullets>()._bulletDamage);
            Destroy(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Store" && waitForSpawn == true)
        {
            var store = collision.GetComponent<Store>();
            //close store
        }
    }
    public void Damage(float damage)
    {
        curHealth -= damage;
        UIManager.instance.UpdateHP();
    }
    public void setShootingAnimOff() => anim.SetBool("isShooting", false);
}