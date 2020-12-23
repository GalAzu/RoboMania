using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 mousePos;
    public float movementSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void Update()
    {
       mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(Input.GetAxis("Vertical"));
    }
    private void FixedUpdate()
    {
        Movement();
        PlayerRotation();

    }
    private void Movement()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            transform.position += transform.up * Time.deltaTime * movementSpeed;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
            transform.position -= transform.up * Time.deltaTime * movementSpeed;
    }

    void PlayerRotation()
    {
        //rotation not based on rigidbody.

        /* Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          difference.Normalize();
          float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
          transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);  */

        //rotation based on rigidbody.

        Vector2 direction = mousePos - rb.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;

        //rotation with perspective camera

        /*  Ray mousray = Camera.main.ScreenPointToRay(Input.mousePosition);
          float midpoint = (transform.position - Camera.main.transform.position).magnitude * 0.5f;
          transform.LookAt(mousray.origin + mousray.direction * midpoint);   */
    }

}
