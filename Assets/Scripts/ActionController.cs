using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ActionController : MonoBehaviour
{
    private const string BUTTON = "Action";

    private Actionable m_activeItem;
    private List<Actionable> m_heldItems;

    private bool m_buttonPressed = false;
    private float m_nearestDistance = float.PositiveInfinity;
    private Actionable m_nearestActionable;

    void PerformActionOnActionable(Actionable actionable)
    {
        foreach (Actionable item in m_heldItems)
        {
            HashSet<string> matchingIds = item.GetAttributes();
            matchingIds.IntersectWith(actionable.GetAttributes());
            if (matchingIds.Count > 0)
            {
                actionable.PerformAction(matchingIds, item);
            }
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
            else if (m_activeItem)
            {
                m_activeItem.PerformAction();
            }
        }

        m_nearestDistance = float.PositiveInfinity;
        m_nearestActionable = null;
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Actionable a = collider.GetComponent<Actionable>();
        if (a)
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
