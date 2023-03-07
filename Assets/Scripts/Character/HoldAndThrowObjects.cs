using UnityEngine;
public class HoldAndThrowObjects : Interactions 
{
    [SerializeField] private Animator anim;
    [SerializeField]
    private bool onHold;
    [SerializeField] 
    public float throwForce;
    [SerializeField]
    private Transform objectPlace;
    private void OnEnable()
    {
        action += HoldObject;
    }
    private void OnDisable()
    {
        action -= HoldObject;
    }
    public override void Update()
    {
        base.Update();
        if (onHold) // TODO Get it out of update
        {
            hit.collider.transform.position = objectPlace.position;
            if (Input.GetKeyUp(KeyCode.Mouse2))
            {
                ThrowObject();
            }
        }
    }
    public void HoldObject()
    {
        onHold = true;
        Rigidbody2D hitRb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        //actionDistance += 1;
        hit.collider.transform.position = objectPlace.position;
        hit.collider.transform.parent = objectPlace;
        hitRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ThrowObject()
    {
        var rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();
        var obj = hit.collider.gameObject.GetComponent<ExplosiveItems>();
       // actionDistance -= 1;
        hit.collider.transform.parent = null;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.AddForce(transform.up * throwForce , ForceMode2D.Impulse);
        onHold = false;
    }
}