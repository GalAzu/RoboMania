using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour
{
    public float speed;
    private Player player;
    private Vector3 shurikenDirection;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        shurikenDir();
        Destroy(this.gameObject, 2);
    }
    private void Update()
    {
        ShurikenMove();
    }
    private void ShurikenMove()
    {
        transform.position += shurikenDirection * speed * Time.deltaTime;

    }
    void shurikenDir()
    {
        if (player.shotDirRight == true)
            shurikenDirection = Vector3.right;
        else
            shurikenDirection = new Vector3(-1, 0, 0);
    }
}
