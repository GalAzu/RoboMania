using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float posX;
    private float posY;
    public float offset;
    private Transform playerToFollow;
    public bool isMoving;
    [SerializeField]private Transform rayPoint;
    [SerializeField]private RaycastHit2D ray;
    private Vector3 rayOrigin;
    public float distanceToStopCam;
    private LayerMask borders = 14;
    public float x_minVal;
    public float x_maxVal;
    public float y_minVal;
    public float y_maxVal;
    [SerializeField]
    private Texture2D cursorTexture;

    void Start()
    {        //Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        playerToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if(playerToFollow != null) CameraIsMoving();

    }
    private void CameraIsMoving()
    {
        isMoving = true;
        posX = playerToFollow.transform.position.x;
        posY = playerToFollow.transform.position.y;
        Vector3 pos = new Vector3(posX, posY, -offset);
        transform.position = pos;
        pos.x = Mathf.Clamp(transform.position.x, x_minVal, x_maxVal);
        pos.y = Mathf.Clamp(transform.position.y, y_minVal, y_maxVal);
        var clampedPos = new Vector3(pos.x, pos.y ,transform.position.z );
        transform.position = clampedPos;



        // Camera.main.orthographicSize = offset
    }
    private void OnDrawGizmos()
    {
        if(playerToFollow != null) Debug.DrawRay(rayPoint.position, rayPoint.up * distanceToStopCam);
    }
    private void RayCastCheck()
    {
        rayPoint.position = transform.localPosition;
        ray = Physics2D.Raycast(rayOrigin, Vector2.up, distanceToStopCam);
        if (ray.collider == null)
        {
            isMoving = true;
            print("nothing");
            CameraIsMoving();
        }
        else
        {
            print("Camera stop here");
            isMoving = false;
        }

    }

}
