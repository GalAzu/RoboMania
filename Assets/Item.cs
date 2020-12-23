using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {

            player.score += Random.Range(100, 150);
            Destroy(this.gameObject);
            Debug.Log("Trigger");


        }
    }

}
