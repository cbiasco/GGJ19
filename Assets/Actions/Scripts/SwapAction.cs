using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Swap Action", menuName = "Actions/Swap Action")]
public class SwapAction : Action
{
    public override void Run(Actionable actionable, Actionable item, ActionController actionController)
    {
        if (item == null)
        {
            Debug.LogError("Swap is not supposed to happen with no other item...");
        }
        if (item != actionController.activeItem)
        {
            Debug.LogError("Right now, I'm not sure if this is supposed to happen.");
        }

        if (actionController.activeItem != actionable)
        {
            actionable.transform.parent = actionController.transform;
            actionable.transform.localPosition = Vector3.zero;
            actionable.transform.localRotation = Quaternion.identity;

            item.transform.parent = null;
            actionController.heldItems.Remove(item);

            actionController.heldItems.Add(actionable);
            actionController.activeItem = actionable;
        }
    }

    public override void Startup(Actionable actionable)
    {
        return;
    }
}
