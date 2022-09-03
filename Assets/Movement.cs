using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public float speed;
    public float sizeSpeed;
    public Vector3 sizeTarget;
    public Vector3 velocity;
    private void Update()
    {
        Move();
        Expand();
    }
    public void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.position += new Vector3(horizontal * speed, vertical * speed, 0) * Time.deltaTime; ;
    }
    public void Expand()
    {
        
        if(Input.GetKey(KeyCode.Z))
        {
            Debug.Log("Expand");
            transform.localScale += new Vector3(1, 1, 1) * sizeSpeed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.X))
        {
            Debug.Log("Contract");
            transform.localScale += new Vector3(1, 1, 1) * -sizeSpeed * Time.deltaTime;
        }
    }
}

