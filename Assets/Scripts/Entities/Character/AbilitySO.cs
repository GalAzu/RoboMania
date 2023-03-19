using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpItem", menuName
= "Items/AbilitySO")]
public class AbilitySO : BulletSO 
{
    public AbilitiesManager.AbilityType abilityType;
    public float homingRadius;
    public GameObject GameObject;
    public Transform shotPos;
}