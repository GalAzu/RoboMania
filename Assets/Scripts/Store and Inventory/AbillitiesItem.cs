using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

[CreateAssetMenu(fileName = "AbilityStoreItem", menuName
= "Items/AbillityStore")]
public class AbillitiesItem : Item 
{

    public enum Abiilities
    {
        FireBall,
        IceAttack,
        ShockWave,
    }
    public Abiilities abillity;

}
