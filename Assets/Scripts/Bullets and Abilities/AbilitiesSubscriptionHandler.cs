using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AbilitiesSubscriptionHandler : MonoBehaviour
{
    public abstract class Ability
    {
        public AbilitySO _abilityData;
        public abstract void OnEnable();
        public abstract void OnDisable();
    }
}


