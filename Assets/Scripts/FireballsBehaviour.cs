using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballsBehaviour : MonoBehaviour
{
    private AbilitySO _abilityData;
    [SerializeField]
    private float distanceToTarget;
    [SerializeField]
    private Collider2D[] targets;
    [SerializeField]
    private int randomTarget;
    public void OnEnable()
    {
        _abilityData = Resources.Load<AbilitySO>("AbilitiesSO/2_Fireball");
    }
    private void Start()
    {
        targets = Physics2D.OverlapCircleAll(transform.position, _abilityData.bulletAreaDamage, _abilityData.Damageables);
        Debug.Log(targets.Length);
        randomTarget = Random.Range(0, targets.Length);
        Destroy(gameObject, _abilityData.destroyTime);
    }
    private void Update()
    {
        if(targets[randomTarget] != null)
        transform.position = Vector2.MoveTowards(transform.position, targets[randomTarget].transform.position, _abilityData._bulletSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
