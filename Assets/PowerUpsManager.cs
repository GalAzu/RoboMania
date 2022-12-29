using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private Character character;
    private ShootingManager shooting;
    public Shield shield;
    private float initSpeed;
    public bool _onShield;
    public static int cooldownTimer;
    public float dashForce;
    public float dashTime;
    private void Awake()
    {
        shooting = GetComponent<ShootingManager>();
        character = GetComponent<Character>();
        shield = GetComponentInChildren<Shield>();
        initSpeed = character.movementSpeed;
    }
    private void Update()
    {
        Dash();
    }
    public IEnumerator ResetStatsTimer(int cooldownTime)
    {
        cooldownTimer = cooldownTime;
        cooldownTimer = cooldownTime;
        while (cooldownTimer > 0)
        {
            cooldownTimer--;
            print("cooldown is:" + cooldownTimer.ToString());
            yield return new WaitForSeconds(1);
        }
        character.movementSpeed = initSpeed;
        shooting.ResetShotRates();
        print("RESET STATS");
        yield return null;
    }
    public void ActivateResetStatTimer(int cooldownTime)
    {
        StartCoroutine(ResetStatsTimer(cooldownTime)); ;
    }
    public void IncreaseFireRate(PowerUps powerup) => shooting.abilityShotRate -= powerup.shotRate;
    public void IncreaseSpeed(PowerUps powerup) => character.movementSpeed += powerup.speedAdded;
    public void Dash()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (dashTime <= 0)
            {
                character.rb.AddForce(transform.up * dashForce, ForceMode2D.Impulse);

            }
        }
    }
    public void AddHP(PowerUps consumable)
    {
        print(consumable.hpAdded.ToString() + " HP ADDED");
        Character.instance.curHealth += consumable.hpAdded;
        if (Character.instance.curHealth > 100) Character.instance.curHealth = 100;
        UIManager.instance.UpdateHP();
    }


}
