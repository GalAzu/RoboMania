using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour , Ienvironment
{
    [SerializeField]
    private bool actionable;
    public float actionDistance;
    public LayerMask ObjectsToHold;
    RaycastHit2D hit;
    Vector3 rayDir;
    [SerializeField] LayerMask bullets;

    void Start()
    {
        actionable = true;
    }

    private void Update()
    {
        rayDir = transform.localPosition;
        hit = Physics2D.Raycast(rayDir, transform.up, actionDistance, ObjectsToHold);
        if (hit.collider != null)
        {
            print("interactable!");
            if (actionable && Input.GetKeyDown(KeyCode.LeftControl))
            {
                print("ActionEnables");
                Action();
            }
            else if (hit.collider.gameObject.transform.parent == this.transform && Input.GetKeyDown(KeyCode.LeftControl))
            {
                OutOfAction();
            }
        }
    }
    public void Action()
    {
        actionDistance += 1;
        hit.collider.transform.parent = this.gameObject.transform;
        actionable = false;
    }

    public void OutOfAction()
    {
        actionDistance -= 1;
        hit.collider.gameObject.transform.parent = null;
        actionable = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayDir, transform.up * actionDistance);
    }
}
