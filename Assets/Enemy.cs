using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        MoveTowardPlayer();
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position ,target.transform.position, moveSpeed);
    }    
}
