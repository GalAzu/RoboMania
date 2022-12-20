using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Traps : MonoBehaviour
{
    public float speed;
    public float sizeSpeed;
    public Vector3 sizeTarget;
    public Vector3 velocity;
    private void Update()
    {
        Move();
        TrapSizeBehaviour();
    }
    // Refactor movement and expansion mechanics for traps on the map
    
    public virtual void Move()
    {
    }
    public virtual void TrapSizeBehaviour()
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

