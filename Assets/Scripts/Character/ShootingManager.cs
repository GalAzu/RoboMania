using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
public class ShootingManager : MonoBehaviour
{
    public enum ActiveAbility { Fireball, Blizzard, Shockwave };
    public enum ActiveShot { DoubleLaser, HeavyShot }
    
    public Character character;
    [FoldoutGroup("Dependencies and Setting")]
    public LayerMask Damagable;
    [FoldoutGroup("Dependencies and Setting")]
    [SerializeField] private float timeToNextShot;
    [FoldoutGroup("Dependencies and Setting")]
    [SerializeField] private float timeToNextHeavyShot;
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
    [Range(0, 350), GUIColor(0.5f, 0.9F, 0, 1)]
    public float abilityShotRate;
    [Space]
    [EnumToggleButtons, GUIColor(0.9f, 0.5F, 0, 1)]
    public ActiveShot activeShotType;
    [Range(0, 150), GUIColor(0.9f, 0.5F, 0, 1)]
    public float lightShotRate;
    [Space]
    [FoldoutGroup("Shot Types"), GUIColor(1, 0.4f, 1, 1)]
    [Range(0, 350)]
    public float doubleShotRate = 100;
    [FoldoutGroup("Shot Types"),Range(0, 350), GUIColor(1, 0.4f, 1, 1)]
    public float heavyShotRate = 150;
    [FoldoutGroup("Shockwave"),GUIColor(0.3f, 0.5f, 1, 1)]
    [Range(0, 90)]
    public float shockwaveRadius;
    [FoldoutGroup("Shockwave"), GUIColor(0.3f, 0.5f, 1, 1)]
    [Range(0, 800)]
    public float shockwaveShotRate;
    [FoldoutGroup("Shockwave"), GUIColor(0.3f, 0.5f, 1, 1)]
    [Range(0,350)]
    public float shockwaveDamage;

    [Title("Heavy Shot", null, TitleAlignments.Centered)]
    public bool canShootLight = true;

    private void Awake()
    {
        character = GetComponent<Character>();
        if (activeShotType == ActiveShot.DoubleLaser) lightShotRate = doubleShotRate;
        else lightShotRate = heavyShotRate;
    }

    private void Update()
    {
        OnShoot();
    }
    private void OnShoot()
    {
        Character.instance.state = Character.PlayerState.Shooting;
        if (Input.GetButton("Fire2") && Time.time >= timeToNextShot)
        {
            ShootAbility();
            character.anim.SetBool("isShooting" , true);
            timeToNextShot = Time.time + abilityShotRate * Time.deltaTime;
        }

        else if (Input.GetButton("Fire1") && canShootLight && Time.time >= timeToNextHeavyShot) ShootLight();
    }

    private void ShootLight()
    {
        switch (activeShotType)
        {
            case (ActiveShot.DoubleLaser):
                GameObject bullet = Instantiate(bulletPrefab, shootPointR.transform.position, Character.instance.transform.rotation);   //TDL Bullets pool.
                GameObject bullet2 = Instantiate(bulletPrefab, shootPointL.transform.position, Character.instance.transform.rotation);
                break;
            case (ActiveShot.HeavyShot):
                GameObject missile = Instantiate(missilePrefab, missilePoint.transform.position, Character.instance.transform.rotation);
                break;
        }
        timeToNextHeavyShot = Time.time + lightShotRate * Time.deltaTime;
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
