using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActionController : MonoBehaviour
{
    private const string USE_BUTTON = "Action";
    private const string GRAB_BUTTON = "Grab";

    private bool m_buttonPressed = false;
    private float m_nearestDistance = float.PositiveInfinity;
    private Actionable m_nearestActionable;

    public Actionable activeItem;
    public HashSet<Actionable> heldItems = new HashSet<Actionable>();

    public AudioSource source;
    public AudioClip clip;
  

    void PerformActionOnActionable(Actionable actionable)
    {
        playClip();
        // If we don't have anything, then we can't perform an action
        if (heldItems.Count == 0)
        {
            return;
        }
        // Pass the most appropriate thing that we have in
        else
        {
            Actionable matchedItem = null;
            HashSet<string> matchingAttribs = null;
            
            foreach (Actionable item in heldItems)
            {
                //Debug.Log("Trying " + item.name + " with " + actionable.name + "...");
                matchingAttribs = item.GetAttributes();
                matchingAttribs.IntersectWith(actionable.GetAttributes());
                if (matchingAttribs.Count > 0)
                {
                    matchedItem = item;
                    break;
                }
            }

            if (matchedItem)
            {
                actionable.PerformAction(matchingAttribs, matchedItem, this);
            }
        }
    }
    void PerformSoloAction(Actionable actionable)
    {
        actionable.PerformAction(this);
    }

    void Update()
    {
        if (GameManager.Instance.inputBlocked)
        {
            return;
        }

        if (Input.GetButtonDown(USE_BUTTON))
        {
            
            if (m_nearestActionable)
            {
                PerformActionOnActionable(m_nearestActionable);
                
            }
        }
        else if (Input.GetButtonDown(GRAB_BUTTON))
        {
            
            if (activeItem)
            {
                PerformSoloAction(activeItem);
            }
            else if (m_nearestActionable)
            {
                PerformSoloAction(m_nearestActionable);
            }
        }

        m_nearestDistance = float.PositiveInfinity;
        m_nearestActionable = null;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Actionable a = collider.GetComponent<Actionable>();
        if (a && !heldItems.Contains(a))
        {
            float distance = Vector2.Distance(transform.position, a.transform.position);
            if (distance < m_nearestDistance)
            {
                m_nearestDistance = distance;
                m_nearestActionable = a;
            }
        }
    }
    public void setSource(AudioClip inputClip)
    {
        clip = inputClip;
        source.clip = inputClip;
    }
    public void playClip()
    {
        source.Play();
    }


}
