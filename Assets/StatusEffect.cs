using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public Enemy enemy;
    public enum statusEffect { Null , Shock , Fire , Poison}
    public statusEffect _statusEffect;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
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
        //Shock effect on 
    }
}
