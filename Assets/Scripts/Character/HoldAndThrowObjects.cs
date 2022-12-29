using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class HoldAndThrowObjects : MonoBehaviour 
{
    [SerializeField] private Animator anim;
    [SerializeField]
    private bool onHold;
    public float actionDistance;
    public LayerMask ObjectsToHold;
    private RaycastHit2D hit;
    private Vector3 rayDir;
    [SerializeField] 
    public LayerMask enemies = 8;
    [SerializeField]
    public UnityEvent OnActionEvent , OutOfActionEvent;
    [SerializeField] private int explosionDamage;
    public float throwForce;
    private Character character;
    [SerializeField]
    private Transform objectPlace;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    private void Update()
    {
        rayDir = transform.localPosition;
        hit = Physics2D.Raycast(rayDir, transform.up, actionDistance, ObjectsToHold);
        if (hit.collider != null)
        {
            if (!onHold && Input.GetKeyDown(KeyCode.Mouse2))
            {
                OnActionEvent.Invoke();
                onHold = true;
            }
            else if (onHold && hit.collider.gameObject.transform.parent == objectPlace.transform && Input.GetKeyDown(KeyCode.Mouse2))
            {
                OutOfActionEvent.Invoke();
                onHold = false;
            }
        }
    }
    public void HoldObject()
    {
        Rigidbody2D hitRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        //actionDistance += 1;
        hit.collider.transform.parent = objectPlace;
        hit.collider.transform.position = objectPlace.position;
        hitRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ThrowObject()
    {
        var rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        var obj = hit.collider.gameObject.GetComponent<ResponsiveObjects>();
       // actionDistance -= 1;
        hit.collider.transform.parent = null;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        obj.isMoving = true;
        rb.AddForce(transform.up * throwForce , ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up * actionDistance);
    }
    public void Explode()
    {
        anim.SetBool("OnExplosion", true);
        character.curHealth -= explosionDamage;
    }
}