using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;

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
    //When true desires go unfulfilled, they turn into needs.

    [Title("Character Setting and Dependencies", null, TitleAlignments.Centered)]
    public bool waitForSpawn;
    [SerializeField]
    private float rotationLerp;
    public enum PlayerState { Slowdown, Walking, Dash, Shooting };
    private Vector2 mousePos;


    private void Awake()
    {
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
    }
    private  void FixedUpdate()
    {
            Movement();
            PlayerRotation();
    }
    #region movement and rotation
    private void Movement()
    {
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * movementSpeed;
        moveVector = Vector3.ClampMagnitude(moveVector, 1);
        transform.position += moveVector;
    }

    private void PlayerRotation()
    {
        //rotation based on transform.
          Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          difference.Normalize();
          float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg -90;
          var targetTransform = Quaternion.Euler(0f, 0f, rotationZ) ;
          transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform, rotationLerp);
    }
    #endregion

    //TODO:Manage store with events that subscribe from different classes.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Store" && waitForSpawn == true) //Store open
        {
            var store = collision.GetComponent<Store>();
            //open store
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
        if(curHealth <= 0)
            Death(); //Create death delegate to subscribe from UI/GameManager class.
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public void setShootingAnimOff() => anim.SetBool("isShooting", false);
}