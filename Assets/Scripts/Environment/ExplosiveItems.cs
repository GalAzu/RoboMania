using UnityEngine;

public class ExplosiveItems : MonoBehaviour
    //STORE ALL OBJECT DATA IN SCRIPTABLE AND LOAD IT FROM CLASS
{
    private LayerMask playerBullets = 10;
    private LayerMask enemyBullets = 12;
    private LayerMask enemyLayer = 8;
    [SerializeField]
    private GameObject[] ItemPool; //TODO Change to scriptable objects
    private GameObject itemToDrop; //ScriptableObject
    [SerializeField]
    private float health;
    [Header("Spatial properties")]
    [SerializeField] private float spatialDamage;
    [SerializeField] private float spatialRadius;
    [Space]
    [Header("Object Properties")]
    [SerializeField] private float throwDamage;
    [SerializeField] public bool isExplosive;
    [SerializeField]
    public float velocityDamageThreshold;
    [SerializeField]
    private GameObject smallExplosion;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerBullets || collision.gameObject.layer == enemyBullets)
        {
            DamageObject();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.magnitude > 0.5f && collision.gameObject.layer == enemyLayer)
        {
            var enemy = collision.gameObject.GetComponent<EnemyStateMachine>();
            enemy.health -= throwDamage;
            DamageObject();
        }
    }
    private void DamageObject()
    {
        rb.velocity *= 0.1f;
        health -= 20;
        if (health <= 0)
        {
            if(isExplosive)
            {
                var vfx = Instantiate(smallExplosion, transform.position,Quaternion.identity);
                RaycastHit2D[] rayHits = Physics2D.CircleCastAll(transform.position, spatialRadius, Vector2.zero);
                foreach(var hit in rayHits)
                {
                    if (hit.collider.tag == "Player")
                        hit.collider.gameObject.GetComponent<Character>().Damage(spatialDamage);
                    else if (hit.collider.tag == "Enemy")
                        hit.collider.gameObject.GetComponent<EnemyStateMachine>().Damage(spatialDamage);
                }
                Destroy(vfx, 1.5f);
               
            }
          Destroy(this.gameObject);
          if (itemToDrop != null) DropRandomItem();
        }
    }
    private GameObject RandomItem()
    {
        var index = Random.Range(0, ItemPool.Length);
        itemToDrop = ItemPool[index];
        return itemToDrop;
    }
    private void DropRandomItem()
    {
        Instantiate(RandomItem(), transform.position, Quaternion.identity);
        Debug.Log("DROP ITEM: " + RandomItem().name);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spatialRadius);
    }
}
