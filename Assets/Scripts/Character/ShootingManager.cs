using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
public class ShootingManager : MonoBehaviour
{
    //TODO:Capsulate all shots data and work toward the capsulation - data holders such as so/strucs
    [HideInInspector]
    public float lightShotRate;
    public enum ActiveShot { DoubleLaser, HeavyShot }
    [SerializeField]
    private float RadiusDamageCheck;
    
    public Character character;
    [FoldoutGroup("Dependencies and Setting")]
    public LayerMask Damagable;
    [FoldoutGroup("Dependencies and Setting")]
    [SerializeField] private float timeToNextLightShot;
    [FoldoutGroup("Dependencies and Setting")]
    public GameObject lightShotPrefab;
    [FoldoutGroup("Dependencies and Setting")]
    public GameObject heavyShotPrefab;
    [FoldoutGroup("Dependencies and Setting")]
    public Transform shootPointR, shootPointL, shootPointC;
    private bool canShootLight;

    [EnumToggleButtons, GUIColor(0.9f, 0.5F, 0, 1)]
    public ActiveShot activeShot;
    [Space]
    [FoldoutGroup("Shot Stats"), GUIColor(1, 0.4f, 1, 1), ShowIf("activeShot", ActiveShot.DoubleLaser)]
    [Range(0, 100)]
    public float doubleShotRate = 100;
    [FoldoutGroup("Shot Stats"),Range(0, 350), GUIColor(1, 0.4f, 1, 1), ShowIf("activeShot", ActiveShot.HeavyShot)]
    public float heavyShotRate = 150;


    private void Awake()
    {
        character = GetComponent<Character>();
        if (activeShot == ActiveShot.DoubleLaser) lightShotRate = doubleShotRate;
        else lightShotRate = heavyShotRate;
        ResetShotRates();
    }

    [Button("Update ability ShotRate")]
    public void ResetShotRates()=>
        lightShotRate = CurLightShotRate() * 0.2f;

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
    private void FixedUpdate()
    {
        OnShoot();
    }
    private void OnShoot()
    {

         if (Input.GetButton("Fire1") && canShootLight && Time.time >= timeToNextLightShot) 
            ShootLight();
    }

    private void ShootLight()
    {
        switch (activeShot)
        {
            case (ActiveShot.DoubleLaser):
                GameObject bullet = Instantiate(lightShotPrefab, shootPointR.transform.position, Character.instance.transform.rotation);   //TDL Bullets pool.
                GameObject bullet2 = Instantiate(lightShotPrefab, shootPointL.transform.position, Character.instance.transform.rotation);
                break;
            case (ActiveShot.HeavyShot):
                GameObject heavyShotObj = Instantiate(heavyShotPrefab, shootPointC.transform.position, Character.instance.transform.rotation);
                break;
        }
        timeToNextLightShot = Time.time + lightShotRate * Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, RadiusDamageCheck);
    }
}
