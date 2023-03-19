using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveBehaviour : MonoBehaviour
{
    [SerializeField]
    private AbilitySO _abilityData;
    public void OnEnable()
    {
        _abilityData = Resources.Load<AbilitySO>("AbilitiesSO/1_Shockwave");
    }

    // Start is called before the first frame update
    void Start()
    {
        Shock();
    }
    private void Shock()
    {
        //SEPERATE BEHVIOUR
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, _abilityData.bulletAreaDamage, _abilityData.Damageables);
        foreach (var collider2D in collider)
        {
            if (collider2D != null)
            {
                var enemyMask = 8;
                if (collider2D.gameObject.layer == enemyMask)
                {
                    var enemy = collider2D.GetComponent<EnemyStateMachine>();
                    float proximity = (transform.position - enemy.transform.position).magnitude;
                    float effect = 1 - (proximity / _abilityData.bulletAreaDamage);
                    enemy.Damage(_abilityData._bulletDamage * effect);
                    // enemy.OnStatusEffect(StatusEffect.statusEffect.Shock);
                }
            }
        }
    }
}

