using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActionController : MonoBehaviour
{
    private const string BUTTON = "Action";

    private bool m_buttonPressed = false;
    private float m_nearestDistance = float.PositiveInfinity;
    private Actionable m_nearestActionable;

    public Actionable activeItem;
    public HashSet<Actionable> heldItems = new HashSet<Actionable>();

    void PerformActionOnActionable(Actionable actionable)
    {
        // If we don't have anything, then don't pass anything in
        if (heldItems.Count == 0)
        {
            actionable.PerformAction(this);
        }
        // Pass the most appropriate thing that we have in
        else
        {
            foreach (Actionable item in heldItems)
            {
                HashSet<string> matchingAttribs = item.GetAttributes();
                matchingAttribs.IntersectWith(actionable.GetAttributes());
                if (matchingAttribs.Count > 0)
                {
                    actionable.PerformAction(matchingAttribs, item, this);
                }
            }

            // TODO: "Error" noise since no actions were applicable?
        }
    }

    void Update()
    {
        if (Input.GetButtonDown(BUTTON))
        {
            if (m_nearestActionable)
            {
                PerformActionOnActionable(m_nearestActionable);
            }
            else if (activeItem)
            {
                activeItem.PerformAction(this);
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
}
