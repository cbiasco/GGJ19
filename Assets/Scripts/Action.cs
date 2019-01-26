using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : ScriptableObject
{
    public abstract void StartupAction();

    public abstract void RunAction();
}
