using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullets : MonoBehaviour
{
    public int _bulletDamage;
    public float _bulletSpeed;


    // Update is called once per frame
    void Update()
    {
        transform.Translate((Vector3.up * _bulletSpeed) * Time.deltaTime, Space.Self);
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            print("player Hit");
            Character.instance.curHealth -= _bulletDamage;
            UIManager.instance.UpdateHP();
            Destroy(this.gameObject);
            if(Character.instance.curHealth <= 0)
            {
                GameManager.instance.GameOver();
            }
        }
        else if(collision.gameObject.layer == 11) //Bullet blocking
        {
            Destroy(this.gameObject);
        }
    }
}
