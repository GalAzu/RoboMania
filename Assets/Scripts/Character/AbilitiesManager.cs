using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class AbilitiesManager:MonoBehaviour
{
    [SerializeField]
    private int curAbilityIndex;

    [SerializeField]
    private AbilityType activeAbility;
    [SerializeField]
    public Collider2D[] arrayCheck;
    public AbilitySO[] abilitiesSO;
    [SerializeField]
    private float chargeTime;

    public enum AbilityType
    {
        Null, // = 0
        Shockwave, // = 1
        Fireballs, // = 2
        Blizzard // = 3
    }
    private void Awake()
    {
        abilitiesSO = Resources.LoadAll<AbilitySO>("AbilitiesSO");
    }
    private void Start()
    {
        curAbilityIndex = (int)activeAbility;
    }
    private void OnValidate()
    {
        curAbilityIndex = (int)activeAbility;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            InstantiateAbility(abilitiesSO[(int)activeAbility]0.GameObject);
        }
        else if (Input.GetMouseButton(1))
        {
            Debug.Log("Charging");
            chargeTime += Time.deltaTime;

        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (chargeTime > 3)
            {
                chargeTime = 0;
                Debug.Log("Charge Ability");
                InstantiateAbility(abilitiesSO[(int)activeAbility].GameObject);
            }
            else
            {
                chargeTime = 0;
                Debug.Log("POOF");
            }

        }
    }
    private void InstantiateAbility(GameObject abilityObj)
    {
        GameObject abilityPrefab = Instantiate(abilityObj, transform.position, Quaternion.identity);
    }
}
