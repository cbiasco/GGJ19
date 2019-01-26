using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Actionable : MonoBehaviour
{
    [SerializeField]
    private ActionSet[] actionSets;

    public HashSet<string> GetAttributes()
    {
        HashSet<string> attributes = new HashSet<string>();
        foreach(ActionSet actionSet in actionSets)
        {
            Debug.Assert(actionSet != null);
            attributes.UnionWith(actionSet.attributes);
        }

        attributes.RemoveWhere((x) => { return x.Length == 0; });
        return attributes;
    }

    public void PerformAction(ActionController actionController)
    {
        PerformAction(new HashSet<string>(), null, actionController);
    }

    public void PerformAction(HashSet<string> matchingAttribs, Actionable item, ActionController actionController)
    {
        // If we don't have an item, then we won't have any attributes to match either
        if (item == null)
        {
            foreach (ActionSet actionSet in actionSets)
            {
                Debug.Assert(actionSet != null);
                if (actionSet.attributes.Length == 0)
                {
                    actionSet.Run(this, null, actionController);
                    return;
                }
            }
        }

        // Look for an exact match!
        foreach (ActionSet actionSet in actionSets)
        {
            Debug.Assert(actionSet != null);
            HashSet<string> intersection = new HashSet<string>(matchingAttribs);
            intersection.IntersectWith(actionSet.attributes);
            if (intersection.SetEquals(matchingAttribs))
            {
                actionSet.Run(this, item, actionController);
                return;
            }
        }
    }

    public void Start()
    {
        foreach (ActionSet actionSet in actionSets)
        {
            Debug.Assert(actionSet != null);
            actionSet.Startup(this);
        }
    }
}
