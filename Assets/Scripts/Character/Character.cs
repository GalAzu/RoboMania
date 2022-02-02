using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public enum PlayerState { Slowdown, Walking, Dash, Shooting };

    [Header("State")]
    [Space]
    public PlayerState state;
    Rigidbody2D rb;
    Vector2 mousePos;
    public float movementSpeed;
    public static Character instance;
    [Space]
    [Header("PlayerStats")]
    [Space]
    public float maxHealth;
    public float curHealth;
    [SerializeField] private float dashLengthInTime;
    [SerializeField] private bool onDash;
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCooldown;
    [SerializeField] private float timeToNextDash;
    [SerializeField] public int cash;
    public bool waitForSpawn;
    public Abillities abillitySelected;
    public delegate void abillityEvent();
    public event abillityEvent onAbillity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        instance = this;
        maxHealth = 100;
        curHealth = 100;
        onDash = false;
        
    }
    private void Update()
    {
       mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       switch(state)
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
    private void FixedUpdate()
    {

        Movement();
        PlayerRotation();
    }
    private void Movement()
    {
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")) * Time.deltaTime * movementSpeed;
        transform.position += moveVector;
        if (state != PlayerState.Dash && Input.GetKeyDown(KeyCode.Q))
        {
            Dash();
        }
        if (state != PlayerState.Dash || state != PlayerState.Shooting)
        {
            transform.position += moveVector;
            state = PlayerState.Walking;

            if (Input.GetAxisRaw("Vertical") > 0)
                transform.position += transform.up * Time.deltaTime * movementSpeed;
            else if (Input.GetAxisRaw("Vertical") < 0)                                                ///Move directionaly to the rotation.
                transform.position -= transform.up * Time.deltaTime * movementSpeed;

        }

    }
    private IEnumerator Dash()
    {
        {
            state = PlayerState.Dash;
            print("Dashing");
            timeToNextDash = Time.time + dashCooldown;
            movementSpeed *= dashForce;
            yield return new WaitForSeconds(dashLengthInTime);
            onDash = false;
            movementSpeed /= dashForce;
            print("dash Enable");
            state = PlayerState.Walking;
        }
    }

    private void PlayerRotation()
    {
        //rotation not based on rigidbody.

          Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          difference.Normalize();
          float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg -90;
          transform.rotation = Quaternion.Euler(0f, 0f, rotationZ) ; 

        //rotation based on rigidbody.

       /* Vector2 direction = mousePos - rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;/*

        //rotation with perspective camera

        /*  Ray mousray = Camera.main.ScreenPointToRay(Input.mousePosition);
          float midpoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
          transform.LookAt(mousray.origin + mousray.direction * midpoint);   */
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Store" && waitForSpawn == true)
        {
          UIManager.instance.OpenAndCloseStore();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Store" && waitForSpawn == true)
        {
            UIManager.instance.OpenAndCloseStore();
        }
    }
    public void Teleport()
    {
        transform.position +=instance.transform.forward  *10;
    }
}