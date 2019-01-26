using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void Startup(Actionable actionable);

    public abstract void Run(Actionable actionable, Actionable item, ActionController actionController);
}
