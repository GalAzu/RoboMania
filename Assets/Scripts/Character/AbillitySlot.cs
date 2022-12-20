using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbillitySlot : MonoBehaviour , IPointerClickHandler
{
    public int abillityID;
    public Sprite icon;
    public Image image;
    private ShootingManager shootingManager;


    private void Awake()
    {
        image = GetComponent<Image>();
        shootingManager = FindObjectOfType<ShootingManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {     
        {
          //  if(shootingManager.shotType == ShootingManager.ShotType.Abillity)
            switch (abillityID)
            {
                case (0): //FIREBALL
                    print("ACTIVATE FIREBALL");
                    break;
                case (1): //ICE
                    print("ACTIVATE ICE");
                    break;
                case (2)://SHOCKWAVE
                    Debug.Log("ACTIVATE SHOCKWAVE");
                    Character.instance.shooting.activeAbility = ShootingManager.ActiveAbility.Shockwave;
                    shootingManager.abilityShotRate = shootingManager.shockwaveShotRate;
                    break;
                case (3): //HEAL
                    print("ACTIVATE HEAL");
                    break;
                case (4): //DASH
                    print("ACTIVATE DASH");
                    break;
                case (5):
                    print("Double Laser");
                    Character.instance.shooting.activeShotType = ShootingManager.ActiveShot.DoubleLaser;
                    Character.instance.shooting.abilityShotRate = shootingManager.doubleShotRate;
                    break;
            }
            Destroy(gameObject);
        }
    }
    public void UpdateAbillity(int id,Sprite _icon)
    {
        abillityID = id ;
        icon = _icon;
        var iconChild = gameObject.transform.Find("ItemIcon");
        Image iconChildImage = iconChild.GetComponentInChildren<Image>();
        iconChildImage.sprite = _icon;
        var fixedColor = iconChildImage.color;
        fixedColor.a = 255f;
        iconChildImage.color = fixedColor;
    }
}
