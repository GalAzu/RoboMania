using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Abillities : Item
{
    public enum Abiilities
    {
        FireBall,
        IceAttack,
        ShockWave,
        Heal
    }
    public Abiilities AbillitiesList;
}
