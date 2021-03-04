using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public int lightBulletDamage;
    public float lightBulletSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.up * lightBulletSpeed) * Time.deltaTime, Space.Self);
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            print("player Hit");
            Character.instance.curHealth -= lightBulletDamage;
            UIManager.instance.UpdateHP();
            Destroy(this.gameObject);
            if(Character.instance.curHealth <= 0)
            {
                GameManager.instance.GameOver();
            }
        }
        else if(collision.gameObject.layer == 11)
        {
            Destroy(this.gameObject);
        }
    }
}
