using System.Collections;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public enum State { Walking, Chasing, Attacking, Defending };

    public float moveSpeed;
    public bool onSlowdown;
    public float health;

    [Header("Attack State")]
    public int physicalDamage;

    [Header("State Machine")]
    public State enemyState;
    public float detectRadius;
    public float attackDistance;
    public float backToWalkDistance;
    public float timeToWalkState;
    public float timeSinceHit;
    [SerializeField] Transform target;
    public LayerMask Players, Environment;
    public float stopChasingTime;
    public bool onCooldown;

    [Header("Prefabs")]
    private Vector3 roamingPos;
    [SerializeField]
    private SpawnManager spawnManager;
    [SerializeField]
    private StatusEffect _statusEffect;
    [SerializeField]
    private Character character;
    private Collider2D detectRadiusCollider;
    private EnemiesAttackManager attackManager;
    private const string CONST_PLAYER_NAME = "Player";
    private const string CONST_SPAWN_MANAGER = "SpawnManager";

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag(CONST_PLAYER_NAME).GetComponent<Character>();
        _statusEffect = GetComponent<StatusEffect>();
        attackManager = GetComponent<EnemiesAttackManager>();
    }


    private void Start()
    {
        InitEnemy();
    }
    private void InitEnemy()
    {
        enemyState = State.Walking;
        roamingPos = GetRoamingPos();

    }
    void Update()
    {
        OnAttackRadius();
        StateMachine();
    }
    private void StateMachine()
    {
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
                if (target != null)
                {
                    RotateToTarget();
                    if (Time.time > timeSinceHit)
                    {
                        enemyState = State.Walking;
                    }
                    if (Vector2.Distance(transform.position, target.transform.position) <= attackDistance)
                    {
                        attackManager.Attack();
                    }
                    else ChaseTarget();

                }
                break;
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
        enemyState = State.Chasing;
        if (target != null)
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }

    private void RotateToTarget()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, RotateTowardsNewPos(target.position), 0.2f);
    }
    private IEnumerator BackToWalkState()
    {
        yield return new WaitForSeconds(stopChasingTime);
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

    private void OnAttackRadius()
    {
        detectRadiusCollider = Physics2D.OverlapCircle(transform.position, detectRadius, Players);

        if (detectRadiusCollider != null)
        {
            Debug.Log("RAY HIT");
            enemyState = State.Chasing;
            target = Character.instance.transform;
            if (Vector3.Distance(transform.position, target.transform.position) <= attackDistance)
            {
                enemyState = State.Attacking;
            }
        }
        if (detectRadiusCollider == null && enemyState == State.Chasing)
        {
            if (target != null)
                target = Character.instance.GetComponent<Transform>();
            if (!onCooldown)
            {
                StartCoroutine(BackToWalkState());
                onCooldown = true;
            }
        }
    }
    
    #endregion
    private void OnDeath()
    {
            Destroy(gameObject);
            GameManager.instance.enemiesDefeatedEvent.Invoke();
            GameManager.instance.enemyCount--;
            UIManager.instance.UpdateEnemyCount();
            //Explosion particles / animation
    }
    public void Damage(float damage)
    {
        if (health <= 0) OnDeath();
        else health -= damage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10) //Player layer
        {
            target = character.transform;
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
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
