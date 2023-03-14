using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AbilitiesManager:MonoBehaviour
{
    [SerializeField]
    public AbilitySO[] abilities;
    public int curAbilityIndex;
    public enum AbilityType
    {
        Null, // = 0
        Shockwave, // = 1
        Fireballs, // = 2
        Blizzard // = 3
    }
    public  void Awake()
    {
        abilities = Resources.LoadAll<AbilitySO>("AbilitiesSO");
    }
    private void OnEnable()
    {
        abilities[1].activationEvent += Shockwave;
        abilities[2].activationEvent += Fireballs;
        abilities[3].activationEvent += Blizzard;
    }
    private void OnDisable()
    {
        abilities[1].activationEvent -= Shockwave;
        abilities[2].activationEvent -= Fireballs;
        abilities[3].activationEvent -= Blizzard;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            abilities[curAbilityIndex].Activation();
        }
    }
    private void Shockwave()
    {
        var abilityData = abilities[1];
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, abilityData.bulletAreaDamage, abilityData.Damageables);
        foreach (var collider2D in collider)
        {
            if (collider2D != null)
            {
                var enemyMask = 8;
                if (collider2D.gameObject.layer == enemyMask)
                {
                    var enemy = collider2D.GetComponent<EnemyStateMachine>();
                    enemy.Damage(abilityData._bulletDamage);
                    // enemy.OnStatusEffect(StatusEffect.statusEffect.Shock);
                }
            }
        }
        var sphere = Instantiate(abilityData.vfx, transform.position, Quaternion.identity);
        Destroy(sphere, 0.3f);

    }
    private void Blizzard()
    {

    }
    private void Fireballs()
    {
        Debug.Log("FIRE");
        var abilityData = abilities[2];
        var Fireball1 = Instantiate(abilityData.vfx, transform.position, Quaternion.identity);
        var Fireball2 = Instantiate(abilityData.vfx, transform.position, Quaternion.identity);
        var Fireball3 = Instantiate(abilityData.vfx, transform.position, Quaternion.identity);
    }
}
