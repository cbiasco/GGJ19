using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PickupDrop Action", menuName = "Actions/PickupDrop Action")]
public class PickupDropAction : Action
{
    private bool held = false;

    public override void Run(Actionable actionable, Actionable item, ActionController actionController)
    {
        if (!held)
        {
            held = true;
            actionable.transform.parent = actionController.transform;
            actionable.transform.localPosition = Vector3.zero;
            actionable.transform.localRotation = Quaternion.identity;

            actionController.activeItem = actionable;
            actionController.heldItems.Add(actionable);
        }
        else
        {
            held = false;
            actionable.transform.parent = null;

            actionController.activeItem = null;
            actionController.heldItems.Remove(actionable);
        }
    }

    public override void Startup(Actionable actionable)
    {
        return;
    }
}
