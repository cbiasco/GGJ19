using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public Broomer holder;
    
    public Collider2D grabRange;
    public Collider2D effectiveRange;

    public Broomer touchingBroomer;

    void Update()
    {
        if (Input.GetButtonDown("Action") && holder == null && touchingBroomer)
        {
            holder = touchingBroomer;
            grabRange.enabled = false;
            effectiveRange.enabled = true;

            transform.parent = holder.transform;
        }
        else if (holder && Input.GetButtonDown("Action"))
        {
            effectiveRange.enabled = false;
            grabRange.enabled = true;
            holder = null;

            transform.parent = null;
        }

        touchingBroomer = null;

        // If we're being held, then we need to do stuff
        if (holder)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        touchingBroomer = collider.GetComponent<Broomer>();
    }
}
