using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsManager : MonoBehaviour
{
    private Character character;
    private ShootingManager shooting;
    public Shield shield;
    private float initSpeed;
    private float initShotRate;
    public bool _onShield;
    public static int cooldownTimer;
    public float dashForce;
    public float dashTime;

    private void Awake()
    {
        shooting = GetComponent<ShootingManager>();
        character = GetComponent<Character>();
        shield = GetComponentInChildren<Shield>();
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
        shooting.abilityShotRate = initShotRate;
        character.movementSpeed = initSpeed;
        print("RESET STATS");
        yield return null;
    }
    public void ActivateResetStatTimer(int cooldownTime)
    {
        StartCoroutine(ResetStatsTimer(cooldownTime)); ;
    }
    public void Dash()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if(dashTime <= 0)
            {
                character.rb.AddForce(transform.up * dashForce, ForceMode2D.Impulse);

            }
        }
    }
}
