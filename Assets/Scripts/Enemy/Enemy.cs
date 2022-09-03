using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State { Walking, Chasing, Attacking, Defending };
    public enum AttackType { RangedLight, RangedHeavy, Melee }
    [Header("Enemy Stats")]
    public int physicalDamage;
    public float bulletSpeed;
    public float meleeAttackRange;
    public float moveSpeed;
    public bool onSlowdown;
    public float health;

    [Header("Attack State")]
    public AttackType attackState;
    [SerializeField] private float timeToNextShot;
    [SerializeField] private float shotRate;
    public Transform heavyBulletPos, lightBulletPos;

    [Header("State Machine")]
    public State enemyState;
    public float detectRadius;
    public float attackDistance;
    public float backToWalkDistance;
    public float timeToWalkState;
    public float timeSinceHit;
    [SerializeField] Transform target;
    public LayerMask Players , Environment;
    public float cooldownTime;
    public bool onCooldown;
    [Header("Prefabs")]
    [SerializeField] private GameObject lightBullet;
    [SerializeField] private GameObject HeaveyBullet;
    private Vector3 roamingPos;
    private bool corutineStarted;
    private SpawnManager spawnManager;


    private Collider2D detectRadiusCollider, meleeRadiusCollider;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();

    }
    private void Start()
    {
        attackState = (AttackType)Random.Range(0, 3);
        timeToNextShot = 0;
        enemyState = State.Walking;
        roamingPos = GetRoamingPos();

    }
    void Update()
    {
        Death();
        onRadiusDetection();
        switch (enemyState)
        {
            case (State.Walking):
                RotateToNewRoamingPos();
                transform.position = Vector3.MoveTowards(transform.position, roamingPos, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, roamingPos) < 1)
                {
                    roamingPos = GetRoamingPos();
                }
                break;
            case (State.Chasing):
                if (target != null)
                {
                    RotateToTarget();
                    ChaseTarget();
                    if (Vector3.Distance(transform.position, target.position) > backToWalkDistance)
                        enemyState = State.Walking;
                }

                break;
            case (State.Attacking):
                if(target != null)
                {
                    RotateToTarget();
                    if (Time.time > timeSinceHit)
                    {
                        enemyState = State.Walking;
                    }
                    if (Vector2.Distance(transform.position, target.transform.position) <= attackDistance)
                    {
                        Attack();
                    }
                    else ChaseTarget();

                }
                break;
        }
    }
    private void onRadiusDetection()
    {
        detectRadiusCollider = Physics2D.OverlapCircle(transform.position, detectRadius, Players);

        if (detectRadiusCollider != null)
        {
            target = Character.instance.transform;
            enemyState = State.Chasing;
            if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
            {
                enemyState = State.Attacking;
            }
        }
        if (detectRadiusCollider == null && enemyState == State.Chasing)
        {
            if(target != null)
            target = Character.instance.GetComponent<Transform>();
            if(!onCooldown)
            {
              StartCoroutine(BackToWalkState());
              onCooldown = true;
            }
        }
        if (meleeRadiusCollider != null && enemyState == State.Attacking)
        {
            // Melee attack Sequence
        }
    }
    #region WalkingStateLogic
    private Vector3 GetRandomDir()
    {
        var position = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        return position;
    }
    private Vector3 GetRoamingPos()
    {
        return GetRandomDir() * Random.Range(-30, 30);
    }
    private Quaternion RotateTowardsNewPos(Vector3 newPos)
    {
        var offset = 90f;
        Vector3 dir = newPos - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.Euler(0, 0, 1 * (angle - offset));
        return newRotation;
    }
    private void RotateToNewRoamingPos()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, RotateTowardsNewPos(roamingPos), 0.2f);
    }

    #endregion
    #region ChasingStateLogic
    private void ChaseTarget()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
    private void RotateToTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, RotateTowardsNewPos(target.position), 0.2f);
    }
    private IEnumerator BackToWalkState()
    {
        yield return new WaitForSeconds(cooldownTime);
        enemyState = State.Walking;
    }

    #endregion
    #region AttackingStateLogic
    private void PhysicalHitOnPlayer()
    {
        Character.instance.curHealth -= physicalDamage;
        UIManager.instance.UpdateHP();
        if (Character.instance.curHealth <= 0)
        {
            GameManager.instance.GameOver();
        }

    }
    private void Attack()
    {
        if(Character.instance != null)
        switch (attackState)
        {
            case (AttackType.Melee):
                ChaseTarget();
                break;
            case (AttackType.RangedHeavy):
                HeavyShot();
                break;
            case (AttackType.RangedLight):
                LightShot();
                break;
        }
    }
    private void LightShot()
    {
        if (Time.time > timeToNextShot)
        {
            var bulletPos = lightBulletPos;
            timeToNextShot = Time.time + shotRate;
            var bullet = Instantiate(lightBullet, bulletPos.position, transform.rotation);
        }
    }
    private void HeavyShot()
    {
        if (Time.time > timeToNextShot)
        {
            var bulletPos = heavyBulletPos;
            timeToNextShot = Time.time + shotRate;
            var bullet = Instantiate(HeaveyBullet, bulletPos.position, transform.rotation);
        }
    }
    public Transform GetTarget()
    {
        if (this.gameObject != null)
        {
            return Character.instance.transform;

        }
        else return null;
    }
    #endregion
    private void Death()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameManager.instance.enemiesDefeatedEvent.Invoke();
            spawnManager.EnemyCount --;
            UIManager.instance.UpdateEnemyCount();
            //Explosion particles / animation
        }
    }

    private IEnumerator Slowdown()
    {
        Character.instance.state = Character.PlayerState.Slowdown;
        onSlowdown = true;
        Character.instance.movementSpeed -=1.5f;
        yield return new WaitForSeconds(15);
        Character.instance.movementSpeed = 5;
        Character.instance.state = Character.PlayerState.Walking;
        onSlowdown = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            target = GetTarget();
            enemyState = State.Attacking;
            timeSinceHit = Time.time + timeToWalkState;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && onSlowdown == false)
        {
            PhysicalHitOnPlayer();
            if (onSlowdown == false)
            {
                StartCoroutine("Slowdown");
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, meleeAttackRange);
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

    public void Damage(float damage)
    {
        health -= damage;
    }

}
