using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public EnemyStateMachine enemy;
    public enum statusEffect { Null , Shock , Fire , Poison}
    public statusEffect _statusEffect;
    private bool onSlowdown;
    private void Awake()
    {
        enemy = GetComponent<EnemyStateMachine>();
    }
    private void Update()
    {
        switch(_statusEffect)
        {
          
        }
    }
    public void SetStatus(statusEffect status)
    {
        _statusEffect = status;
    }
    
    private void ShockEffect()
    {

    }
    private IEnumerator Slowdown()
    {
        onSlowdown = true;
        Character.instance.movementSpeed -= 1.5f;
        yield return new WaitForSeconds(15);
        Character.instance.movementSpeed = 5;
        onSlowdown = false;
    }

}
