using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionSet", menuName = "Actions/ActionSet", order = 1)]
public class ActionSet : ScriptableObject
{
    public string[] attributes;
    public Action[] actions;

    public void Startup(Actionable actionable)
    {
        foreach (Action action in actions)
        {
            Debug.Assert(action != null);
            action.Startup(actionable);
        }
    }
    public void Run(Actionable actionable, Actionable item, ActionController actionController)
    {
        foreach (Action action in actions)
        {
            Debug.Assert(action != null);
            action.Run(actionable, item, actionController);
        }
    }
}
