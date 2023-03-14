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
        shootingManager = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<ShootingManager>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {     
        {
          //  if(shootingManager.abilityType == ShootingManager.abilitType.x)
            switch (abillityID)
            {
                case (0): //FIREBALL
                    break;
                case (1): //ICE
                    break;
                case (2)://SHOCKWAVE
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
