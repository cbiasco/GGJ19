using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mop Action", menuName = "Actions/Mop Action")]
public class MopAction : Action
{
    public override void Run(Actionable actionable, Actionable item, ActionController actionController)
    {
        // The messes are actually the things running the code...
    }

    public override void Startup(Actionable actionable)
    {
        // Nothing!
    }
}
