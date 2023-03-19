using System;
using UnityEngine;

public class Interactions : MonoBehaviour
{
    protected Vector2 rayDir;
    protected RaycastHit2D hit;
    [SerializeField]
    protected float actionDistance;
    public LayerMask Interactables = 9;
    protected Action action;
    protected Character character;
    private void Awake()
    {
        character = GetComponent<Character>();
    }
    public virtual void Update()
    {
        rayDir = transform.localPosition;
        hit = Physics2D.Raycast(rayDir, transform.up, actionDistance, Interactables);
        if (hit.collider != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse2)) // On Action
                action?.Invoke();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up * actionDistance);
    }
}
