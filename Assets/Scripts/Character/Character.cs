using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Character : MonoBehaviour
{
    public float dashForce;

    [HideInInspector] 
    public static int cooldownTimer;
    public bool waitForSpawn;
    public ShootingManager shooting;
    public static Character instance;
    public GameObject shield;
    [HideInInspector]
    public Animator anim;
    public enum PlayerState { Slowdown, Walking, Dash, Shooting };
    public PlayerState state;
    Rigidbody2D rb;
    Vector2 mousePos;
    [Space]
    [Header("PlayerStats")]
    public float movementSpeed;
    public float maxHealth;
    public float curHealth;

    [SerializeField] 
    public int machineParts;
    [Space]
    [Header("PowerUps")]
    public float initSpeed;
    public float initShotRate;
    public bool _onShield;


    private void Awake()
    {
        shooting = FindObjectOfType<ShootingManager>();
        initSpeed = movementSpeed;
        initShotRate = shooting.lightShotRate;
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
        if(Input.GetKeyDown(KeyCode.P))
        {
            ShieldPowerSwitch();
        }


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
    private  void FixedUpdate()
    {
        Movement();
        PlayerRotation();
    }
    #region movement
    private void Movement()
    {
        Dash();
        Vector3 moveVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.deltaTime * movementSpeed;
        moveVector = Vector3.ClampMagnitude(moveVector, 1);
        transform.position += moveVector;
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
    #endregion
    #region PowerUps
    private void ShieldPowerSwitch()
    {
        _onShield = true ? true : false;
        shield.SetActive(_onShield);
        print(_onShield.ToString());
    }

    public void Dash ()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            print("DASH");
            var upVector = transform.up * dashForce;
            var newPos = transform.position + upVector;
            transform.position = Vector3.MoveTowards(transform.position, newPos , 0.5f);
        }
    }
    public IEnumerator ResetStatsTimer(int cooldownTime)
    {
        cooldownTimer = cooldownTime;
        cooldownTimer = cooldownTime;
        while (cooldownTimer > 0)
        {
            cooldownTimer--;
            print("cooldown is:" + cooldownTimer.ToString());
            yield return new WaitForSeconds(1);
        }
        shooting.lightShotRate = initShotRate;
        movementSpeed = initSpeed;
        print("RESET STATS");
        yield return null;
    }
    public void ActivateResetStatTimer(int cooldownTime)
    {
        StartCoroutine(ResetStatsTimer(cooldownTime)); ;
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Store" && waitForSpawn == true)
        {
            var store = collision.GetComponent<Shop>();
            UIManager.instance.OpenAndCloseStore(store);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Store" && waitForSpawn == true)
        {
            var store = collision.GetComponent<Shop>();
            UIManager.instance.OpenAndCloseStore(store);
        }
    }
    public void Damage(float damage)
    {
        curHealth -= damage;
        UIManager.instance.UpdateHP();
    }

    public void setShootingAnimOff() => anim.SetBool("isShooting", false);
}