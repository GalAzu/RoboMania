using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpItem", menuName
= "Items/AbilitySO")]
public class AbilitySO : BulletSO 
{
    public AbilitiesManager.AbilityType abilityType;
    public GameObject GameObject;
}