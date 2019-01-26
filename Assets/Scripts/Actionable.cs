using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actionable : MonoBehaviour
{
    public readonly ActionSet[] actionSets;

    public HashSet<string> GetAttributes()
    {
        HashSet<string> attributes = new HashSet<string>();
        foreach(ActionSet actionSet in actionSets)
        {
            attributes.UnionWith(actionSet.attributes);
        }

        return attributes;
    }

    public void PerformAction()
    {
        PerformAction(new HashSet<string>(), null);
    }

    public void PerformAction(HashSet<string> matchingIds, Actionable item)
    {
        
    }
}
