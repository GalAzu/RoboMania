using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpItem", menuName
= "Items/AbilitySO")]
public class AbilitySO : BulletSO 
{
    public AbilitiesManager.AbilityType abilityType;
    public delegate void Activate();
    public event Activate activationEvent;

    public void Activation()
    {

        activationEvent?.Invoke();
    }
}