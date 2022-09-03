using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ObjectInteractions : MonoBehaviour 
{
    [SerializeField]
    private bool onAction;
    public float actionDistance;
    public LayerMask ObjectsToHold;
    private RaycastHit2D hit;
    private Vector3 rayDir;
    [SerializeField] 
    public LayerMask enemies = 8;
    [SerializeField]
    public UnityEvent OnActionEvent , OutOfActionEvent;
    public float throwForce;



    private void Update()
    {
        rayDir = transform.localPosition;
        hit = Physics2D.Raycast(rayDir, transform.up, actionDistance, ObjectsToHold);
        if (hit.collider != null)
        {
            if (!onAction && Input.GetKeyDown(KeyCode.Mouse2))
            {
                OnActionEvent.Invoke();
                onAction = true;
            }
            else if (onAction && hit.collider.gameObject.transform.parent == this.transform && Input.GetKeyDown(KeyCode.Mouse2))
            {
                OutOfActionEvent.Invoke();
                onAction = false;
            }
        }
    }
    public void HoldObject()
    {
        actionDistance += 1;
        hit.collider.transform.parent = gameObject.transform;
    }

    public void ThrowObject()
    {
        var rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        var obj = hit.collider.gameObject.GetComponent<ResponsiveObjects>();
        actionDistance -= 1;
        hit.collider.transform.parent = null;
        obj.isMoving = true;
        rb.AddForce(transform.up * throwForce , ForceMode2D.Impulse);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up * actionDistance);
    }
}