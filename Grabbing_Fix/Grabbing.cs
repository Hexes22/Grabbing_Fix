using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Rigidbody2D hand;
    [Range(0, 1)] public int isLeftOrRight;
    
    private GameObject currentlyHolding;
    [HideInInspector]public bool canGrab;
    private bool isGrabbing;

    private FixedJoint2D joint;
    private List<GameObject> connectedStuff = new List<GameObject>();

    private void Update()
    {
        if (Input.GetMouseButtonDown(isLeftOrRight))
        {
            canGrab = true;
        }
        if (Input.GetMouseButtonUp(isLeftOrRight))
        {
            canGrab = false;
        }
            
        if (!canGrab && currentlyHolding != null)
        {
            isGrabbing = false;
            FixedJoint2D[] joints = currentlyHolding.GetComponents<FixedJoint2D>();
            for (int i = 0; i < joints.Length; i++)
            {
                if (joints[i].connectedBody == hand)
                {
                    Destroy(joints[i]);
                }
            }

            joint = null;
            currentlyHolding = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
      if (canGrab && isGrabbing == false && col.gameObject.GetComponent<Rigidbody2D>() != null)//grabs
        {
            connectedStuff.Add(col.gameObject);

            currentlyHolding = col.gameObject;
            joint = currentlyHolding.AddComponent<FixedJoint2D>();
            joint.connectedBody = hand;
            joint.enableCollision = true;

            isGrabbing = true;
        }
    }
}