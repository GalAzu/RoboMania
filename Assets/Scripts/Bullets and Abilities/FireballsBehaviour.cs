using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballsBehaviour : MonoBehaviour
{
    private AbilitySO _abilityData;
    [SerializeField]
    private Collider2D[] targets;
    [SerializeField]
    public GameObject explosionVFX;
    private int randomTarget;
    public void OnEnable()
    {
        _abilityData = Resources.Load<AbilitySO>("AbilitiesSO/2_Fireball");
    }
    private void Start()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, _abilityData.homingRadius, _abilityData.Damageables);
        Debug.Log(targets.Length);
        randomTarget = Random.Range(0, targets.Length);
    }
    private void Update()
    {
        if (targets[randomTarget] != null)
            transform.position = Vector2.MoveTowards(transform.position, targets[randomTarget].transform.position, _abilityData._bulletSpeed * Time.deltaTime);
        else
            transform.Translate(Vector2.up * _abilityData._bulletSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Invoke("HitRaycast", _abilityData.destroyTime);
    }
    private void HitRaycast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position,_abilityData.bulletAreaDamage,_abilityData.Damageables);
        if (colliders != null)
        {
            foreach (Collider2D collider in colliders)
            {
                // linear falloff of effect
                float proximity = (transform.position - collider.transform.position).magnitude;
                float effect = 1 - (proximity / _abilityData.bulletAreaDamage);
                if (effect > 0)
                {
                    collider.gameObject.GetComponent<Entity>().Damage(_abilityData._bulletDamage * effect);
                    float damage = _abilityData._bulletDamage * effect;
                    Debug.Log("" + collider.name + "$has recieved damage of$" + damage);
                }
                var vfx =  Instantiate(explosionVFX, transform.position,Quaternion.identity);
                Destroy(gameObject);
                Destroy(vfx, 1.5f);
            }
        }
    }
}
