using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
public class ShootingManager : MonoBehaviour
{
    [HideInInspector]
    public float lightShotRate;
    [HideInInspector]
    public float abilityShotRate;

    public enum ActiveAbility { Fireball, Blizzard, Shockwave };
    public enum ActiveShot { DoubleLaser, HeavyShot }
    
    public Character character;
    [FoldoutGroup("Dependencies and Setting")]
    public LayerMask Damagable;
    [FoldoutGroup("Dependencies and Setting"),ShowInInspector]
    [SerializeField] private float timeToNextAbilityShot;
    [FoldoutGroup("Dependencies and Setting")]
    [SerializeField] private float timeToNextLightShot;
    [FoldoutGroup("Dependencies and Setting")]
    public GameObject bulletPrefab;
    [FoldoutGroup("Dependencies and Setting")]
    public GameObject missilePrefab;
    [FoldoutGroup("Dependencies and Setting")]
    public Transform shootPointR, shootPointL, missilePoint;
    [FoldoutGroup("Dependencies and Setting")]
    public GameObject ElectricitySphere;

    [TitleGroup("Ability",null,TitleAlignments.Centered,horizontalLine:true,boldTitle:true,indent:false)]

    [EnumToggleButtons , GUIColor(0.5f,0.9F,0,1)]
    public ActiveAbility activeAbility;
    [Space]
    [EnumToggleButtons, GUIColor(0.9f, 0.5F, 0, 1)]
    public ActiveShot activeShot;
    [Space]
    [FoldoutGroup("Shot Stats"), GUIColor(1, 0.4f, 1, 1), ShowIf("activeShot", ActiveShot.DoubleLaser)]
    [Range(0, 100)]
    public float doubleShotRate = 100;
    [FoldoutGroup("Shot Stats"),Range(0, 350), GUIColor(1, 0.4f, 1, 1), ShowIf("activeShot", ActiveShot.HeavyShot)]
    public float heavyShotRate = 150;
    [FoldoutGroup("Shockwave Stats"),GUIColor(0.3f, 0.5f, 1, 1),ShowIf("activeAbility",ActiveAbility.Shockwave)]
    [Range(0, 90)]
    public float shockwaveRadius;
    [FoldoutGroup("Shockwave Stats"), GUIColor(0.3f, 0.5f, 1, 1), ShowIf("activeAbility", ActiveAbility.Shockwave)]
    [Range(0, 500)]
    public float shockwaveShotRate;
    [FoldoutGroup("Shockwave Stats"), GUIColor(0.3f, 0.5f, 1, 1), ShowIf("activeAbility", ActiveAbility.Shockwave)]
    [Range(0,350)]
    public float shockwaveDamage;
    [ShowInInspector]
    private bool canShootLight = true;
    [FoldoutGroup("Blizzard Stats"), GUIColor(0.2f, 2.5f, 1, 1), ShowIf("activeAbility", ActiveAbility.Blizzard)]
    public float blizzardShotRate;
    private float fireBallShotRate;


    private void Awake()
    {
        ResetShotRates();
        abilityShotRate = curAbilityShotRate();
        character = GetComponent<Character>();
        if (activeShot == ActiveShot.DoubleLaser) lightShotRate = doubleShotRate;
        else lightShotRate = heavyShotRate;
    }

    [Button("Update ability Shotrate")]
    public void ResetShotRates()
    {
        abilityShotRate = curAbilityShotRate() * 0.2f;
        lightShotRate = CurLightShotRate() * 0.2f;
    }

    private float CurLightShotRate()
    {
        switch(activeShot)
        {
            case (ActiveShot.DoubleLaser):
                return doubleShotRate;
            case (ActiveShot.HeavyShot):
                return heavyShotRate;
        }
        return lightShotRate;
    }

    public float curAbilityShotRate()
    {
        switch (activeAbility)
        {
            case (ActiveAbility.Shockwave):
                return shockwaveShotRate;
            case (ActiveAbility.Blizzard):
                return blizzardShotRate;
            case (ActiveAbility.Fireball):
                return fireBallShotRate;
        }
        return abilityShotRate;
    }

    private void Update()
    {
        OnShoot();
    }
    private void OnShoot()
    {
        character.state = Character.PlayerState.Shooting;
        if (Input.GetButton("Fire2"))
        {
            if (Time.time >= timeToNextAbilityShot)
            {
                ShootAbility();
                character.anim.SetBool("isShooting", true);
                timeToNextAbilityShot = Time.time + abilityShotRate * Time.deltaTime;
            }
            else character.anim.SetTrigger("Cooldown");
        }

        else if (Input.GetButton("Fire1") && canShootLight && Time.time >= timeToNextLightShot) ShootLight();
    }


    private void ShootLight()
    {
        switch (activeShot)
        {
            case (ActiveShot.DoubleLaser):
                GameObject bullet = Instantiate(bulletPrefab, shootPointR.transform.position, Character.instance.transform.rotation);   //TDL Bullets pool.
                GameObject bullet2 = Instantiate(bulletPrefab, shootPointL.transform.position, Character.instance.transform.rotation);
                break;
            case (ActiveShot.HeavyShot):
                GameObject missile = Instantiate(missilePrefab, missilePoint.transform.position, Character.instance.transform.rotation);
                break;
        }
        timeToNextLightShot = Time.time + lightShotRate * Time.deltaTime;
    }
    private void ShootAbility()
    {
        switch(activeAbility)
        {
            case (ActiveAbility.Shockwave):
                Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, shockwaveRadius , Damagable );
                foreach(var collider2D in collider)
                {
                    if(collider2D != null)
                    {
                        var enemyMask = 8;
                        if(collider2D.gameObject.layer == enemyMask)
                        {
                            var enemy = collider2D.GetComponent<Enemy>();
                            enemy.Damage(shockwaveDamage);
                        }
                    }
                }
                var sphere = Instantiate(ElectricitySphere, transform.position , Quaternion.identity);
                Destroy(sphere, 0.3f);
                break;
            case (ActiveAbility.Blizzard):
                break;
            case (ActiveAbility.Fireball):
                break;
        }
    }
    private void OnDrawGizmosSelected()
    {
       Gizmos.color = Color.yellow;
       Gizmos.DrawWireSphere(transform.position, shockwaveRadius);
    }
}
