using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mess Action", menuName = "Actions/Mess Action")]
public class MessAction : Action
{
    private string m_messType;

    public override void Run(Actionable actionable, Actionable item, ActionController actionController)
    {
        GameManager.Instance.RemoveMess(m_messType);
        Destroy(actionable.gameObject);
    }

    public override void Startup(Actionable actionable)
    {
        m_messType = actionable.GetComponent<SpriteRenderer>().sprite.name;
        GameManager.Instance.AddMess(m_messType);
    }
}