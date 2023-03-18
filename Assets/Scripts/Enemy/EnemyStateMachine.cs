using System.Collections;
using UnityEngine;

public class EnemyStateMachine : Entity
{
    public enum State { Walking, Chasing, Attacking, Defending };
    public State enemyState;

    [Header("State Machine")]
    [Header("Chasing")]
    public float stopChasingTime;
    public float backToWalkDistance;
    [Header("Walking")]
    public float detectRadius;
    public float moveSpeed;
    [Header("Attacking")]
    [SerializeField]
    private LayerMask playerLayer;


    [Header("Prefabs and References")]
    [SerializeField] Transform target;
    private Vector3 roamingPos;
    [SerializeField]
    private Character character;
    private Collider2D detectRadiusCollider;
    private EnemiesAttackManager attackManager;
    private const string CONST_PLAYER_NAME = "Player";
    private const string CONST_SPAWN_MANAGER = "SpawnManager";

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag(CONST_PLAYER_NAME).GetComponent<Character>();
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
        StateMachine();
    }
    private void StateMachine()
    {
        OnAttackRadius();
        switch (enemyState)
        {
            case (State.Walking):
                RotateToNewRoamingPos();
                GoToNextPos();
                break;
            case (State.Chasing):
                if (target != null)
                {
                    RotateToTarget();
                    ChaseTarget();
                    CheckReturnToWalk();
                }
                break;
            case (State.Attacking):
                if (target != null)
                {
                    RotateToTarget();
                    if (Vector2.Distance(transform.position, target.transform.position) <= attackManager.attackDistance && target != null)
                    {
                        if(attackManager != null)
                        attackManager.Attack();
                    }
                    else ChaseTarget();

                }
                break;
        }
    }
    private void OnDeath()
    {
            Destroy(gameObject);
            GameManager.instance.enemiesDefeatedEvent.Invoke();
            GameManager.instance.enemyCount--;
            UIManager.instance.UpdateEnemyCount();
            //Explosion particles / animation
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            PhysicalHitOnPlayer();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
    #region WalkingStateLogic
    public static Vector3 GetRandomDir()
    {
        var position = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        return position;
    }
    public static Vector3 GetRoamingPos()
    {
        return GetRandomDir() * Random.Range(-30, 30);
    }
    private void GoToNextPos()
    {
        if (Vector3.Distance(transform.position, roamingPos) < 0.5f)
        {
            roamingPos = GetRoamingPos();
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, roamingPos, moveSpeed * Time.deltaTime);
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
        if(target != null)
        target = null;
    }

    #endregion
    #region AttackingStateLogic
    private void PhysicalHitOnPlayer() //TODO Refactor to event
    {
        character.curHealth -= attackManager.physicalDamage;
        UIManager.instance.UpdateHP();
        if (character.curHealth <= 0)
        {
            GameManager.instance.GameOver();
        }

    }
    private void CheckReturnToWalk()
    {
        if (target != null && Vector3.Distance(transform.position, target.position) > backToWalkDistance)
            target = character.transform;
        else
            StartCoroutine(BackToWalkState());
    }

    private void OnAttackRadius() //Check attack radius
    {
        detectRadiusCollider = Physics2D.OverlapCircle(transform.position , detectRadius, playerLayer);

        if (detectRadiusCollider != null)
        {
            enemyState = State.Chasing;
            target = character.transform;
            if (Vector3.Distance(transform.position, target.transform.position) <= attackManager.attackDistance)
            {
                enemyState = State.Attacking;
            }
        }
    }

    #endregion

}
